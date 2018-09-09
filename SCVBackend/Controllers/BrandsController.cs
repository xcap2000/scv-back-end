using EFSecondLevelCache.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCVBackend.Domain;
using SCVBackend.Infrastructure;
using SCVBackend.Model;
using System.Linq;
using System.Threading.Tasks;

namespace SCVBackend.Controllers
{
    [Authorize(Roles = "Customer, Seller, Admin")]
    [Route("api/[controller]")]
    public class BrandsController : Controller
    {
        private readonly ScvContext scvContext;

        public BrandsController(ScvContext scvContext)
        {
            this.scvContext = scvContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await scvContext.Brands
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

            return Ok(products);
        }
    }
}