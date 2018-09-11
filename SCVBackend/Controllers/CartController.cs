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
using SCVBackend.Infrastructure;
using SCVBackend.Model;

namespace SCVBackend.Controllers
{
    [Authorize(Roles = "Customer, Seller")]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly ScvContext scvContext;

        public CartController(ScvContext scvContext)
        {
            this.scvContext = scvContext;
        }

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

        [HttpPost]
        [Route("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutModel checkoutModel)
        {
            using (var transaction = scvContext.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var order = await scvContext.Orders
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
                    State = checkoutModel.City,
                    Country = checkoutModel.Country,
                    PostalCode = checkoutModel.PostalCode,
                    CreditCardNumber = checkoutModel.CreditCardNumber,
                    VerificationCode = checkoutModel.VerificationCode
                };

                scvContext.OrderDetails.Add(orderDetails);

                await scvContext.SaveChangesAsync();

                transaction.Commit();

                return Ok(orderNumber);
            }
        }

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