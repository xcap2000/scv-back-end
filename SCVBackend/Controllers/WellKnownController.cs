using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace SCVBackend.Controllers
{
    public class WellKnownController : Controller
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/.well-known/acme-challenge/cHgYUXKGty80coyF9dSG3FtSmCX2Tb2eTzawoAaYklM")]
        public IActionResult Index()
        {
            return File(Encoding.UTF8.GetBytes("cHgYUXKGty80coyF9dSG3FtSmCX2Tb2eTzawoAaYklM.fCta6qCK-oFf6d_ExTBGy0868GJ2a_r_H8IzWF4UNWk"), "text/plain");
        }
    }
}