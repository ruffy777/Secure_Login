using API.Helper;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting();
builder.Services.AddMvc();


// Add services to the container.

builder.Services.AddControllers()
    .AddOData(opt => opt.AddRouteComponents("", ApplicationEdmModel.GetEdmModel()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1.0",
        new OpenApiInfo { Title = "api.seclogin.ch", Version = "v1.0" });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis:6379"; // redis is the container name of the redis service. 6379 is the default port
    options.InstanceName = "SampleInstance";
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "security";
    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "api.seclogin.ch");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
