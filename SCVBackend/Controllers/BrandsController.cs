using EFSecondLevelCache.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCVBackend.Domain;
using SCVBackend.Infrastructure;
using SCVBackend.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCVBackend.Controllers
{
    [Route("api/[controller]")]
    public class BrandsController : Controller
    {
        private readonly ScvContext scvContext;

        public BrandsController(ScvContext scvContext)
        {
            this.scvContext = scvContext;
        }

        /// <summary>
        ///     Lists brands.
        /// </summary>
        /// <returns>
        ///     A list of brands.
        /// </returns>
        [ProducesResponseType(200)]
        [Produces(typeof(List<BrandListModel>))]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var brands = await scvContext.Brands
                .OrderBy(p => p.Name)
                .Select
                (
                    b => new BrandListModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Logo = b.Logo.ToBase64()
                    }
                )
                .Cacheable()
                .ToListAsync();

            return Ok(brands);
        }
    }
}