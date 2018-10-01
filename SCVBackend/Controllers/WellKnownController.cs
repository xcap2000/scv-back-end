using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace SCVBackend.Controllers
{
    public class WellKnownController : Controller
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/.well-known/acme-challenge/U_r0VPKasEwRQN_GQ2p9DTIIW89pJexz-9O5dGEOJq4")]
        public IActionResult Index()
        {
            return File(Encoding.UTF8.GetBytes("U_r0VPKasEwRQN_GQ2p9DTIIW89pJexz-9O5dGEOJq4.fCta6qCK-oFf6d_ExTBGy0868GJ2a_r_H8IzWF4UNWk"), "text/plain");
        }
    }
}