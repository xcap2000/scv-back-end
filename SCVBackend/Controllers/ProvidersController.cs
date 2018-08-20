using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCVBackend.Domain;
using SCVBackend.Model;
using System.Linq;
using System.Threading.Tasks;

namespace SCVBackend.Controllers
{
    [Route("api/[controller]")]
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
            var providersQuery = scvContext.Providers
                .Where(p => filter != null ? p.Name.Contains(filter) || p.BaseApiUrl.Contains(filter) : true);

            var providersCount = await providersQuery
                .CountAsync();

            var providers = await providersQuery
                .OrderBy(p => p.Name)
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

            return Ok(new PagedResult<ProviderListModel>(providersCount, providers));
        }
    }
}
