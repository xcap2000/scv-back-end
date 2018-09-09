using EFSecondLevelCache.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCVBackend.Domain;
using SCVBackend.Domain.Entities;
using SCVBackend.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SCVBackend.Controllers
{
    [Authorize(Roles = "Admin")]
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
                .Cacheable()
                .ToListAsync();

            return Ok(new PagedResult<ProviderListModel>(providersCount, providers));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid? id = null)
        {
            ProviderEditModel providerEditModel;

            if (id == null)
            {
                providerEditModel = new ProviderEditModel();

                return Ok(providerEditModel);
            }

            var provider = await scvContext.Providers
                .Cacheable()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (provider == null)
            {
                return NotFound();
            }

            providerEditModel = new ProviderEditModel
            {
                Id = provider.Id,
                Name = provider.Name,
                BaseApiUrl = provider.BaseApiUrl
            };

            return Ok(providerEditModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [
                FromBody,
                Bind
                (
                    nameof(ProviderCreateModel.Name),
                    nameof(ProviderCreateModel.BaseApiUrl)
                )
            ] ProviderCreateModel providerCreateModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var provider = new Provider(Guid.NewGuid(), providerCreateModel.Name, providerCreateModel.BaseApiUrl);

            scvContext.Add(provider);
            await scvContext.SaveChangesAsync();

            providerCreateModel.Id = provider.Id;

            return Created($"providers/{providerCreateModel.Id}", providerCreateModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            [FromRoute] Guid id,
            [
                FromBody,
                Bind
                (
                    nameof(ProviderEditModel.Id),
                    nameof(ProviderEditModel.Name),
                    nameof(ProviderEditModel.BaseApiUrl)
                )
            ] ProviderEditModel providerEditModel)
        {
            if (!ModelState.IsValid || id != providerEditModel.Id)
                return BadRequest(ModelState);

            var provider = await scvContext.Providers
                .Cacheable()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (provider == null)
            {
                return NotFound();
            }

            provider.Name = providerEditModel.Name;
            provider.BaseApiUrl = providerEditModel.BaseApiUrl;

            await scvContext.SaveChangesAsync();

            return Ok(providerEditModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var provider = await scvContext.Providers
                .Cacheable()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (provider == null)
            {
                return NotFound();
            }

            scvContext.Providers.Remove(provider);

            await scvContext.SaveChangesAsync();

            return Ok();
        }
    }
}