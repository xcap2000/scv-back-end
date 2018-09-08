using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SCVBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        // TODO - When loading order for current cart update all order items with products latest sell price.
        public IActionResult Index()
        {
            return View();
        }
    }
}