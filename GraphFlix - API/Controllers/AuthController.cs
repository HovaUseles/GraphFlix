using Microsoft.AspNetCore.Mvc;

namespace GraphFlix.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
