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

        /// <summary>
        ///     Lists providers, allows filtering and requires paging.
        /// </summary>
        /// <param name="filter">
        ///     Used to filter providers based on their names or URLs.
        /// </param>
        /// <param name="page">
        ///     The desired page to be retrieved.
        /// </param>
        /// <param name="itemsPerPage">
        ///     The desired items per page to be retrieved.
        /// </param>
        /// <returns>
        ///     A list of 
        /// </returns>
        [ProducesResponseType(200)]
        [Produces(typeof(PagedResult<ProviderListModel>))]
        [HttpGet]
        public async Task<IActionResult> Get(string filter = null, int page = 1, int itemsPerPage = 10)
        {
            var providersQuery = scvContext.Providers
                .Where(p => filter != null ? p.Name.Contains(filter) || p.BaseApiUrl.Contains(filter) : true);

            var providersCount = await providersQuery
                .Cacheable()
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

        /// <summary>
        ///     Gets the provider to be edited/deleted.
        /// </summary>
        /// <param name="id">
        ///     The provider's id.
        /// </param>
        /// <returns>
        ///     The provider's to be edited/deleted.
        /// </returns>
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(ProviderEditModel))]
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
                .Where(p => p.Id == id)
                .Cacheable()
                .SingleOrDefaultAsync();

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

        /// <summary>
        ///     Creates an new provider.
        /// </summary>
        /// <param name="providerCreateModel">
        ///     The model representing the provider to be added.
        /// </param>
        /// <returns>
        ///     The added provider with the generated id.
        /// </returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [Produces(typeof(ProviderCreateModel))]
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
            {
                return BadRequest(ModelState);
            }

            var provider = new Provider(Guid.NewGuid(), providerCreateModel.Name, providerCreateModel.BaseApiUrl);

            scvContext.Add(provider);

            await scvContext.SaveChangesAsync();

            providerCreateModel.Id = provider.Id;

            return Created($"providers/{providerCreateModel.Id}", providerCreateModel);
        }

        /// <summary>
        ///     Edits a provider.
        /// </summary>
        /// <param name="id">
        ///     The id of the provider to be edited.
        /// </param>
        /// <param name="providerEditModel">
        ///     The model representing the provider to be edited.
        /// </param>
        /// <returns>
        ///     The edited provider.
        /// </returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(ProviderEditModel))]
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
            {
                return BadRequest(ModelState);
            }

            var provider = await scvContext.Providers
                .Where(p => p.Id == id)
                .Cacheable()
                .SingleOrDefaultAsync();

            if (provider == null)
            {
                return NotFound();
            }

            provider.Name = providerEditModel.Name;
            provider.BaseApiUrl = providerEditModel.BaseApiUrl;

            scvContext.Providers.Update(provider);

            await scvContext.SaveChangesAsync();

            return Ok(providerEditModel);
        }

        /// <summary>
        ///     Deletes a provider.
        /// </summary>
        /// <param name="id">
        ///     The provider's id.
        /// </param>
        /// <returns></returns>
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var provider = await scvContext.Providers
                .Where(p => p.Id == id)
                .Cacheable()
                .SingleOrDefaultAsync();

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