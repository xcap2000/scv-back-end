using SCVBackend.Domain.Entities;
using System.Threading.Tasks;

namespace SCVBackend.ExternalServices
{
    public interface IProviderService
    {
        Task SellProductsAsync(Order order);
    }
}