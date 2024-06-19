using API.Helper;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting();
builder.Services.AddMvc();

builder.Services.AddAuthorization();
// Add services to the container.

builder.Services.AddControllers()
    .AddOData(opt => opt.AddRouteComponents("", ApplicationEdmModel.GetEdmModel()));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis:6379"; // redis is the container name of the redis service. 6379 is the default port
    options.InstanceName = "SampleInstance";
});

string connectionString = builder.Configuration.GetConnectionString("API");
builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(connectionString));
var app = builder.Build();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}"); });
app.UseODataBatching();



app.UseAuthorization();

app.MapControllers();

app.Run();
