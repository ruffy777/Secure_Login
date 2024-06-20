using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using seclogin.Helpers;
using seclogin.Models;
using System.Web;

namespace seclogin.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IApiHelper _apiHelper;
        private readonly HashHelper _hashHelper;

        public LoginController(IConfiguration configuration, IApiHelper apiHelper, HashHelper hashHelper)
        {
            _configuration = configuration;
            _apiHelper = apiHelper;
            _hashHelper = hashHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login([FromBody] UserCredential userCredential)
        {
            if(userCredential == null) { return BadRequest("data is empty"); }

            var email = userCredential.Email;
            var passwortInput = userCredential.Passwort;

            bool result = await Authenticate(email, passwortInput);

            if(result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public async Task<bool> Authenticate(string email, string passwort) {

            var encodedEmail = HttpUtility.UrlPathEncode(email);

            var baseUrl = _configuration.GetValue<string>("API:Url");
            var url = $"{baseUrl}/UserCredential?$filter=Email eq '{encodedEmail}'";

            var (success, response) = await _apiHelper.Get(url);
            if(success)
            {
                var data = JsonConvert.DeserializeObject<List<UserCredential>>(response);

                var pwd = data[0].Passwort;
                var id = data[0].pk_UserCredential.ToString();

                var passwortInputHash = _hashHelper.PasswordHash(passwort, id);

                if(passwortInputHash == pwd)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
