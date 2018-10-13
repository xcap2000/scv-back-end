using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace SCVBackend.Controllers
{
    public class WellKnownController : Controller
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/.well-known/acme-challenge/PmVBZYphnA9H2a5FtLidEKzxOlIvIx-tY7OOD_EQspg")]
        public IActionResult Index()
        {
            return File(Encoding.UTF8.GetBytes("PmVBZYphnA9H2a5FtLidEKzxOlIvIx-tY7OOD_EQspg.fCta6qCK-oFf6d_ExTBGy0868GJ2a_r_H8IzWF4UNWk"), "text/plain");
        }
    }
}