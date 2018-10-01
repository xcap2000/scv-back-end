using Microsoft.AspNetCore.Mvc;
using SCVBackend.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using SCVBackend.Model;
using SCVBackend.Infrastructure;
using EFSecondLevelCache.Core;
using Microsoft.EntityFrameworkCore;
using SCVBackend.Domain.Entities;
using System.Collections.Generic;

namespace SCVBackend.Controllers
{
    [Route("api/[controller]")]
    public class StoreController : Controller
    {
        private readonly ScvContext scvContext;

        public StoreController(ScvContext scvContext)
        {
            this.scvContext = scvContext;
        }

        /// <summary>
        ///     Gets the products available to sell.
        /// </summary>
        /// <param name="userId">
        ///     The user's id when signed in.
        /// </param>
        /// <param name="brandId">
        ///     The brand's id when filtering by brand.
        /// </param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [Produces(typeof(List<StoreListModel>))]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid? userId = null, [FromQuery] Guid? brandId = null)
        {
            var storeListModels = await scvContext.Products
                .Where(p => brandId != null ? p.BrandId == brandId : true)
                .OrderBy(p => p.Name)
                .Select
                (
                    p => new StoreListModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.SellPrice,
                        CanSell = p.Quantity > 0,
                        Photo = p.Photo.ToBase64(),
                        InCart = p.OrderItems.Any(o => userId != null ? o.Order.UserId == userId && o.Order.OrderStatus == OrderStatus.Open : true)
                    }
                )
                .Cacheable()
                .ToListAsync();

            return Ok(storeListModels);
        }
    }
}