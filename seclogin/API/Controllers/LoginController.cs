using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace API.Controllers
{
    public class LoginController : ODataController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
