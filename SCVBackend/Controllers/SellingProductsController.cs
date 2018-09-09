using Microsoft.AspNetCore.Mvc;
using SCVBackend.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using SCVBackend.Model;
using SCVBackend.Infrastructure;
using EFSecondLevelCache.Core;
using Microsoft.EntityFrameworkCore;
using SCVBackend.Domain.Entities;

namespace SCVBackend.Controllers
{
    [Authorize(Roles = "Customer, Seller")]
    [Route("api/selling-products")]
    public class SellingProductsController : Controller
    {
        private readonly ScvContext scvContext;

        public SellingProductsController(ScvContext scvContext)
        {
            this.scvContext = scvContext;
        }

        [HttpGet("{userId:Guid}/{brandId:Guid?}")]
        public async Task<IActionResult> Get(Guid userId, Guid? brandId = null)
        {
            var sellingProducts = await scvContext.Products
                .Where(p => brandId != null ? p.BrandId == brandId : true)
                .OrderBy(p => p.Name)
                .Select
                (
                    p => new SellingProductListModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.SellPrice,
                        CanSell = p.Quantity > 0,
                        Photo = p.Photo.ToBase64(),
                        InCart = p.OrderItems.Any(o => o.Order.UserId == userId && o.Order.OrderStatus == OrderStatus.Open)
                    }
                )
                .Cacheable()
                .ToListAsync();

            return Ok(sellingProducts);
        }
    }
}