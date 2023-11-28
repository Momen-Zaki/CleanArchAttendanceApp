using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CleanArchAttendanceApp.Core;
using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Infrastructure;
using CleanArchAttendanceApp.Infrastructure.DbContexts;
using CleanArchAttendanceApp.Infrastructure.Services;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

string? connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
builder.Services.AddDbContext<AttendanceContext>(options =>
          options.UseSqlite(connectionString));

builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();

builder.Services.AddFastEndpoints();
//builder.Services.AddFastEndpointsApiExplorer();
builder.Services.SwaggerDocument(o =>
{
    o.ShortSchemaNames = true;
    o.DocumentSettings = s =>
    {
        s.Title = "Employee Attendance Management System App Api";
        s.Version = "v1";
    };
    o.EnableJWTBearerAuth = true;
});

var JWTkey = builder.Configuration["JwtBearerDefaults:SecretKey"];
builder.Services.AddJWTBearerAuth(JWTkey!);
builder.Services.AddAuthorization();

//builder.Services.AddSwaggerGen(c =>
//{
//  c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
//  c.EnableAnnotations();
//  string xmlCommentFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "swagger-docs.xml");
//  c.IncludeXmlComments(xmlCommentFilePath);
//  c.OperationFilter<FastEndpointsOperationFilter>();
//});

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
    config.Services = new List<ServiceDescriptor>(builder.Services);

    // optional - default path to view services is /listallservices - recommended to choose your own path
    config.Path = "/listservices";
});


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DefaultCoreModule());
    containerBuilder.RegisterModule(new AutofacInfrastructureModule(builder.Environment.IsDevelopment()));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
}
else
{
    app.UseDefaultExceptionHandler(); // from FastEndpoints
    app.UseHsts();
}

app.UseFastEndpoints();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerGen(); // FastEndpoints middleware

app.UseHttpsRedirection();

app.Run();

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program
{
}
