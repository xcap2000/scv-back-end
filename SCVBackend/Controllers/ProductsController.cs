using Microsoft.AspNetCore.Mvc;
using SCVBackend.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SCVBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ScvContext scvContext;

        public ProductsController(ScvContext scvContext)
        {
            this.scvContext = scvContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid? brandId = null)
        {
            // TODO - Continue here.
            /*
            var productsQuery = scvContext.Products
                .Where(p => brandId != null ? p.ProviderId == brandId : true);

            var products = await productsQuery
                .OrderBy(p => p.Name)
                .Select
                (
                    p => new ProviderListModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        BaseApiUrl = p.BaseApiUrl
                    }
                )
                .Cacheable()
                .ToListAsync();

            return Ok(products);
            */
            throw new NotImplementedException();
        }
    }
}