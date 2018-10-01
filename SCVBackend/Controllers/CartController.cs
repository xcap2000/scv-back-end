using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EFSecondLevelCache.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCVBackend.Domain;
using SCVBackend.Domain.Entities;
using SCVBackend.ExternalServices;
using SCVBackend.Infrastructure;
using SCVBackend.Model;

namespace SCVBackend.Controllers
{
    [Authorize(Roles = "Customer, Seller")]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly ScvContext scvContext;

        private readonly IProviderService providerService;

        public CartController(ScvContext scvContext, IProviderService providerService)
        {
            this.scvContext = scvContext;

            this.providerService = providerService;
        }

        /// <summary>
        ///     Gets the cart of a specific user.
        /// </summary>
        /// <param name="userId">
        ///     The id of the user to get the cart.
        /// </param>
        /// <returns>
        ///     The user's cart.
        /// </returns>
        [ProducesResponseType(200)]
        [Produces(typeof(CartModel))]
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var order = await scvContext.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(o => o.Product)
                .Where(o => o.UserId == userId && o.OrderStatus == OrderStatus.Open)
                .Cacheable()
                .SingleOrDefaultAsync();

            if (order == null)
            {
                return Ok();
            }

            var cart = new CartModel
            {
                Id = order.Id,
                Total = order.Total,
                CartItems = order.OrderItems.OrderBy(o => o.Product.Name).Select
                (
                    o =>
                    new CartItemModel
                    {
                        Id = o.Id,
                        Photo = o.Product.Photo.ToBase64(),
                        ProductName = o.Product.Name,
                        Quantity = o.Quantity,
                        Price = o.Price,
                        Subtotal = o.Subtotal
                    }
                )
                .ToList()
            };

            return Ok(cart);
        }

        /// <summary>
        ///     Adds a product to the user's cart.
        /// </summary>
        /// <param name="addToCartModel">
        ///     Model representing the user's cart and product to be added.
        /// </param>
        /// <returns>
        ///     A model representing a cart's item.
        /// </returns>
        [ProducesResponseType(200)]
        [Produces(typeof(CartItemModel))]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddToCartModel addToCartModel)
        {
            var order = await scvContext.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(o => o.Product)
                .Where(o => o.UserId == addToCartModel.UserId && o.OrderStatus == OrderStatus.Open)
                .Cacheable()
                .SingleOrDefaultAsync();

            if (order == null)
            {
                order = new Order
                (
                    Guid.NewGuid(),
                    OrderStatus.Open,
                    addToCartModel.UserId
                );
                scvContext.Orders.Add(order);
            }

            var product = await scvContext.Products
                .Where(p => p.Id == addToCartModel.ProductId)
                .Cacheable()
                .SingleAsync();

            var orderItem = new OrderItem(Guid.NewGuid(), 1, product.SellPrice, order.Id, product.Id);

            scvContext.OrderItems.Add(orderItem);

            await scvContext.SaveChangesAsync();

            var cartItemModel = new CartItemModel
            {
                Id = orderItem.Id,
                ProductName = product.Name,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price
            };

            return Ok(cartItemModel);
        }

        /// <summary>
        ///     Performs the checkout of an order.
        /// </summary>
        /// <param name="checkoutModel">
        ///     Model representing the payment and delivery information.
        /// </param>
        /// <returns>
        ///     Returns the generated order number.
        /// </returns>
        [ProducesResponseType(200)]
        [Produces(typeof(long))]
        [HttpPost]
        [Route("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutModel checkoutModel)
        {
            using (var transaction = scvContext.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var order = await scvContext.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.Id == checkoutModel.CartId)
                    .Cacheable()
                    .SingleAsync();

                var orderNumber = await scvContext.Orders
                    .MaxAsync(o => o.OrderNumber) ?? 0;

                order.OrderNumber = ++orderNumber;
                order.OrderStatus = OrderStatus.Closed;
                order.CloseDate = DateTime.Now;

                scvContext.Orders.Update(order);

                var orderDetails = new OrderDetails
                {
                    OrderId = checkoutModel.CartId,
                    Street = checkoutModel.Street,
                    City = checkoutModel.City,
                    State = checkoutModel.State,
                    Country = checkoutModel.Country,
                    PostalCode = checkoutModel.PostalCode,
                    CreditCardNumber = checkoutModel.CreditCardNumber,
                    VerificationCode = checkoutModel.VerificationCode
                };

                scvContext.OrderDetails.Add(orderDetails);

                await scvContext.SaveChangesAsync();

                await providerService.SellProductsAsync(order);

                transaction.Commit();

                return Ok(orderNumber);
            }
        }


        /// <summary>
        ///     Updates a product in the user's cart.
        /// </summary>
        /// <param name="updateCartItemModel">
        ///     Model representing the user's cart item to be updated.
        /// </param>
        /// <returns>
        ///     A boolean whether the process succeeded.
        /// </returns>
        [ProducesResponseType(200)]
        [Produces(typeof(bool))]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCartItemModel updateCartItemModel)
        {
            var orderItem = await scvContext.OrderItems
                .Where(o => o.Id == updateCartItemModel.CartItemId)
                .Cacheable()
                .SingleAsync();

            orderItem.Quantity = updateCartItemModel.Quantity;

            scvContext.OrderItems.Update(orderItem);

            await scvContext.SaveChangesAsync();

            return Ok(true);
        }

        /// <summary>
        ///     Deletes an cart's item.
        /// </summary>
        /// <param name="cartItemId">
        ///     The cart's item to be deleted.
        /// </param>
        /// <returns>
        ///     A boolean whether the process succeeded.
        /// </returns>
        [ProducesResponseType(200)]
        [Produces(typeof(bool))]
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> Delete(Guid cartItemId)
        {
            var order = await scvContext.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.OrderItems.Any(i => i.Id == cartItemId))
                .Cacheable()
                .SingleOrDefaultAsync();

            var orderItemToRemove = order.OrderItems.Single(i => i.Id == cartItemId);

            scvContext.OrderItems.Remove(orderItemToRemove);

            if (order.OrderItems.Count() == 1)
            {
                scvContext.Orders.Remove(order);
            }

            await scvContext.SaveChangesAsync();

            return Ok(true);
        }
    }
}