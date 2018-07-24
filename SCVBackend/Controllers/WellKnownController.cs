using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace SCVBackend.Controllers
{
    public class WellKnownController : Controller
    {
        [Route("/.well-known/acme-challenge/1oJfhdhjvnT8wwbyJ6b8-S_7qjEcfUHHitqPeTEHlQY")]
        public IActionResult Domain()
        {
            return File(Encoding.UTF8.GetBytes("1oJfhdhjvnT8wwbyJ6b8-S_7qjEcfUHHitqPeTEHlQY.5L4sA11CpZAIg_CZuZtDvB7wjHxTBGV5plDRWt_aurs"), "text/plain");
        }

        [Route("/.well-known/acme-challenge/c_2dZYJVQ8ush2rUdynZL57zUGTrsUgisFenTu1C390")]
        public IActionResult DomainWithW3()
        {
            return File(Encoding.UTF8.GetBytes("c_2dZYJVQ8ush2rUdynZL57zUGTrsUgisFenTu1C390.5L4sA11CpZAIg_CZuZtDvB7wjHxTBGV5plDRWt_aurs"), "text/plain");
        }
    }
}