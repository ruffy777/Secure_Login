using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using API.Model;
using API.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using admin.api.Extensions;
using Microsoft.Identity.Client;

namespace API.Controllers
{
    [EnableQuery]
    public class LoginController : ODataController
    {
        private readonly ApiDbContext _context;
        public LoginController(ApiDbContext context)
        {
            _context = context;
        }


        public IQueryable<UserCredential> Get()
        {
            return _context.UserCredential.AsQueryable();
        }




        public async Task<IActionResult> Post([FromBody] UserCredential userCredential)
        {
            if(userCredential == null) return BadRequest(string.Empty);

            var list = userCredential.GetSqlParameterList(false);

            var sParams = list.GetSqlParameterString();

            var sql = "EXEC usp_PostUserCredential " + sParams;
            var result = (await _context.UserCredential.FromSqlRaw(sql, list.ToArray<object>()).ToListAsync())
                .First();
            return Ok(result);
            
        }



        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            var find = await _context.UserCredential.FindAsync(key);
            if (find == null) return BadRequest("key not found");

            var list = new List<SqlParameter> { new("@pk_UserCredential", key) };
            var sParams = list.GetSqlParameterString();
            var sql = "EXEC usp_DeleteUserCredential " + sParams;
            var result = _context.Database.ExecuteSqlRaw(sql, list.ToArray<object>());
            return Ok(result);
        }


        public async Task<IActionResult> Patch([FromBody] UserCredential userCredential, [FromODataUri] Guid key)
        {
            var find = await _context.UserCredential.FindAsync(key);
            if (find == null) return BadRequest("key not found");
            if (userCredential == null) return BadRequest("invalid passed data");

            var list = userCredential.GetSqlParameterList(true);
            var pk_UC = new SqlParameter("@pk_UserCredential", key);
            list.Add(pk_UC);

            var sParams = list.GetSqlParameterString();

            var sql = "EXEC usp_PatchUserCredential " + sParams;

            var result = (await _context.UserCredential.FromSqlRaw(sql, list.ToArray<object>()).ToListAsync())
                .First();
            return Ok(result);

        }
    }
}
