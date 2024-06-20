using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace API.Controllers
{
    public class TokenController : ODataController
    {
        public readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //[AllowAnonymous]
        //public ActionResult Token([FromBody] LoginRequest login)
        //{
        //    var token = GenerateToken();
        //    return Ok(token);
        //}
    }
}
