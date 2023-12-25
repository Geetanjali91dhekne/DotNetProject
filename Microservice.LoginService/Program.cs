
using AuthenticationService.Models;
using AuthenticationService.Repositories;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<FleetManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString"))
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();

//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
 //  .AddNegotiate();
//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = options.DefaultPolicy;
//});
var app = builder.Build();


if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
