using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCVBackend.Domain;
using SCVBackend.Model;
using System.Linq;
using System.Threading.Tasks;

namespace SCVBackend.Controllers
{
    public class ProvidersController : Controller
    {
        private readonly ScvContext scvContext;

        public ProvidersController(ScvContext scvContext)
        {
            this.scvContext = scvContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var providers = await scvContext.Providers
                .Select
                (
                    p => new ProviderListModel
                    {
                        Id = p.Id,
                        Name = p.Name
                    }
                )
                .ToListAsync();

            return Ok(providers);
        }
    }
}
