using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using seclogin.Helpers;
using seclogin.Models;

namespace seclogin.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IApiHelper _apiHelper;
        private readonly HashHelper _hashHelper;

        public RegisterController(IConfiguration configuration, IApiHelper apiHelper, HashHelper hashHelper)
        {
            _configuration = configuration;
            _apiHelper = apiHelper;
            _hashHelper = hashHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Post([FromBody] UserCredential userCredential)
        {
            if (userCredential == null) { return BadRequest("Data is empty"); }
            userCredential.pk_UserCredential = Guid.NewGuid();

            var passworthash = _hashHelper.PasswordHash(userCredential.Passwort, userCredential.pk_UserCredential.ToString());
            userCredential.Passwort = passworthash;

            try
            {
                var baseUrl = _configuration.GetValue<string>("API:Url");
                var url = $"{baseUrl}/UserCredential";
                var jsonData = JsonConvert.SerializeObject(userCredential);
                var (success, response) = await _apiHelper.Post(jsonData, url);

                if (success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return BadRequest(response);
                }

            }catch (Exception ex)
            {
                return BadRequest("did not work");
            }
        }
    }
}
