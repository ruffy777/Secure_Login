using Microsoft.AspNetCore.Mvc;

namespace seclogin.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
