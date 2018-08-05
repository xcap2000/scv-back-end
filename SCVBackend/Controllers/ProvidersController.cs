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
        public async Task<IActionResult> Get(string filter = null, int page = 1, int itemsPerPage = 10)
        {
            var providers = await scvContext.Providers
                .Where(p => filter != null ? p.Name.Contains(filter) || p.BaseApiUrl.Contains(filter) : true)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select
                (
                    p => new ProviderListModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        BaseApiUrl = p.BaseApiUrl
                    }
                )
                .ToListAsync();

            return Ok(providers);
        }
    }
}
