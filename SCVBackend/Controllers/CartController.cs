using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFSecondLevelCache.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCVBackend.Domain;
using SCVBackend.Domain.Entities;
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

        [HttpGet]
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
                return Ok(null);
            }

            var cart = new CartModel
            {
                Id = order.Id,
                Total = order.Total,
                CartItems = order.OrderItems.Select
                (
                    o =>
                    new CartItemModel
                    {
                        Id = o.Id,
                        ProductName = o.Product.Name,
                        Quantity = o.Quantity,
                        Price = o.Price
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

            var product = await scvContext.Products.SingleAsync(p => p.Id == addToCartModel.ProductId);

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
    }
}