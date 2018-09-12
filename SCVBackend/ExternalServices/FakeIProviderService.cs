using SCVBackend.Domain;
using SCVBackend.Domain.Entities;
using System.Threading.Tasks;
using System.Linq;
using EFSecondLevelCache.Core;
using Microsoft.EntityFrameworkCore;

namespace SCVBackend.ExternalServices
{
    /* This class simulates an external provider/supplier service that when invoked by http would call back this API
     * using webhooks in order to update the system stock copy. For simplicity I do not call the external service nor its implementation
     * exists. The product id is assumed to be the same as the provider's one and the implementation does not reflect reality as I am using
     * a database context here to achieve the desired result. 
     */
    public class FakeIProviderService : IProviderService
    {
        private readonly ScvContext scvContext;

        public FakeIProviderService(ScvContext scvContext)
        {
            this.scvContext = scvContext;
        }

        public async Task SellProductsAsync(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                var product = await scvContext.Products
                    .Where(p => p.Id == orderItem.ProductId)
                    .Cacheable()
                    .SingleAsync();

                product.Quantity -= orderItem.Quantity;

                scvContext.Products.Update(product);

                await scvContext.SaveChangesAsync();
            }
        }
    }
}
