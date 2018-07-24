using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace SCVBackend.Controllers
{
    public class WellKnownController : Controller
    {
        [Route("/.well-known/acme-challenge/1oJfhdhjvnT8wwbyJ6b8-S_7qjEcfUHHitqPeTEHlQY")]
        public IActionResult Index()
        {
            return File(Encoding.UTF8.GetBytes("1oJfhdhjvnT8wwbyJ6b8-S_7qjEcfUHHitqPeTEHlQY.5L4sA11CpZAIg_CZuZtDvB7wjHxTBGV5plDRWt_aurs"), "text/plain");
        }
    }
}