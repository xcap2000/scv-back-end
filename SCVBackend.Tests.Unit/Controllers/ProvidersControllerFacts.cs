using Microsoft.AspNetCore.Mvc;
using SCVBackend.Controllers;
using SCVBackend.Domain;
using SCVBackend.Model;
using SCVBackend.Tests.Unit.TestInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static Xunit.Assert;

namespace SCVBackend.Tests.Unit.Controllers
{
    public class ProvidersControllerFacts
    {
        public class TheGetMethod : TestBase
        {
            private readonly ScvContext scvContext;

            private readonly ProvidersController providersController;

            public TheGetMethod()
            {
                scvContext = CreateContext();

                providersController = new ProvidersController(scvContext);
            }

            [Fact]
            public async void ReturnsItemsNotNull()
            {
                var result = await providersController.Get() as OkObjectResult;

                var items = result.Value;

                NotNull(items);
            }

            [Fact]
            public async void ReturnsItemsEmpty()
            {
                var result = await providersController.Get() as OkObjectResult;

                var items = result.Value as IEnumerable<ProviderListModel>;

                Empty(items);
            }

            [Fact]
            public async void ReturnsItemsNotEmpty()
            {
                UsingContext(c =>
                {
                    c.Add(new Provider(Guid.NewGuid(), "Provider 1", "https://provider1.com/api"));
                });

                var result = await providersController.Get() as OkObjectResult;

                var list = result.Value as IEnumerable<ProviderListModel>;

                NotEmpty(list);
            }

            [Fact]
            public async void ReturnsItemsHydrated()
            {
                var provider = new Provider(Guid.NewGuid(), "Provider 1", "https://provider1.com/api");

                UsingContext(c =>
                {
                    c.Add(provider);
                });

                var result = await providersController.Get() as OkObjectResult;

                var list = result.Value as IEnumerable<ProviderListModel>;

                var item = list.Single();

                Equal(provider.Id, item.Id);
                Equal(provider.Name, item.Name);
                Equal(provider.BaseApiUrl, item.BaseApiUrl);
            }

            [Fact]
            public async void ReturnsItemsFilteredByName()
            {
                var provider1 = new Provider(Guid.NewGuid(), "Provider 1", "https://provider1.com/api");
                var provider2 = new Provider(Guid.NewGuid(), "Provider 2", "https://provider2.com/api");

                UsingContext(c =>
                {
                    c.Add(provider1);
                    c.Add(provider2);
                });

                var result = await providersController.Get("Provider 2") as OkObjectResult;

                var list = result.Value as IEnumerable<ProviderListModel>;

                var item = list.Single();

                Equal(provider2.Id, item.Id);
                Equal(provider2.Name, item.Name);
                Equal(provider2.BaseApiUrl, item.BaseApiUrl);
            }

            [Fact]
            public async void ReturnsItemsFilteredByApiUrl()
            {
                var provider1 = new Provider(Guid.NewGuid(), "Provider 1", "https://provider1.com/api");
                var provider2 = new Provider(Guid.NewGuid(), "Provider 2", "https://provider2.com/api");

                UsingContext(c =>
                {
                    c.Add(provider1);
                    c.Add(provider2);
                });

                var result = await providersController.Get("provider1") as OkObjectResult;

                var list = result.Value as IEnumerable<ProviderListModel>;

                var item = list.Single();

                Equal(provider1.Id, item.Id);
                Equal(provider1.Name, item.Name);
                Equal(provider1.BaseApiUrl, item.BaseApiUrl);
            }

            [Fact]
            public async void ReturnsItemsPaged()
            {
                var provider1 = new Provider(Guid.NewGuid(), "Provider 1", "https://provider1.com/api");
                var provider2 = new Provider(Guid.NewGuid(), "Provider 2", "https://provider2.com/api");

                UsingContext(c =>
                {
                    c.Add(provider1);
                    c.Add(provider2);
                });

                var result = await providersController.Get(page: 2, itemsPerPage: 1) as OkObjectResult;

                var list = result.Value as IEnumerable<ProviderListModel>;

                var item = list.Single();

                Equal(provider2.Id, item.Id);
                Equal(provider2.Name, item.Name);
                Equal(provider2.BaseApiUrl, item.BaseApiUrl);
            }
        }
    }
}