using LogbookManagementService.Models;
using LogbookManagementService.Repositories;
using LogbookManagementService.Scheduler;
using LogbookManagementService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!))
        };
    });
builder.Services.AddDbContext<FleetManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString"))
);
builder.Services.AddDbContext<CRMSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCRMSConnectionString"))
);
builder.Services.AddDbContext<DatamartMobileContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDatamartConnectionString"))
);
builder.Services.AddDbContext<OMSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyOmsMobileConnectionString"))
);
builder.Services.AddDbContext<OMSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyOmsMobileConnectionString"))
);
builder.Services.AddDbContext<IDRVContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyIDRVConnectionString"))
);
builder.Services.AddScoped<ILogbookService, LogbookService>();
builder.Services.AddScoped<ILogbookRepository, LogbookRepository>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddHostedService<QuartzHostedService>();
builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

builder.Services.AddSingleton<AssetPoolScheduler>();

builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(AssetPoolScheduler),
    cronExpression: "0 0 9 * * ?"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
