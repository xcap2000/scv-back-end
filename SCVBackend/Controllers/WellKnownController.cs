using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace SCVBackend.Controllers
{
    public class WellKnownController : Controller
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/.well-known/acme-challenge/IVmQbDLRLb4TZJT_m0__xrY85YVW2ntW2qX1S7Jy61Y")]
        public IActionResult Index()
        {
            return File(Encoding.UTF8.GetBytes("IVmQbDLRLb4TZJT_m0__xrY85YVW2ntW2qX1S7Jy61Y.fCta6qCK-oFf6d_ExTBGy0868GJ2a_r_H8IzWF4UNWk"), "text/plain");
        }
    }
}