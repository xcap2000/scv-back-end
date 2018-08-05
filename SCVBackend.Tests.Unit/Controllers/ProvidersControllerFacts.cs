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
                    c.Add(new Provider(Guid.NewGuid(), "Fornecedor 1", "https://fornecedor1.com/api"));
                });

                var result = await providersController.Get() as OkObjectResult;

                var list = result.Value as IEnumerable<ProviderListModel>;

                NotEmpty(list);
            }

            [Fact]
            public async void ReturnsItemsHydrated()
            {
                var provider = new Provider(Guid.NewGuid(), "Fornecedor 1", "https://fornecedor1.com/api");

                UsingContext(c =>
                {
                    c.Add(provider);
                });

                var result = await providersController.Get() as OkObjectResult;

                var list = result.Value as IEnumerable<ProviderListModel>;

                var item = list.Single();

                Equal(provider.Id, item.Id);
                Equal(provider.Name, item.Name);
            }
        }
    }
}