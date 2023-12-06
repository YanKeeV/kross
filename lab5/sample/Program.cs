using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Hosting;
using SampleMvcApp;
using System.Net;

using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddMemoryCache();

ConfigureServices(builder.Services);

var app = builder.Build();

Configure(app, builder.Configuration);

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();

    services.AddAuth0WebAppAuthentication(options =>
    {
        options.Domain = builder.Configuration["Auth0:Domain"];
        options.ClientId = builder.Configuration["Auth0:ClientId"];
    });

    services.Configure<CookiePolicyOptions>(options =>
    {
        options.MinimumSameSitePolicy = SameSiteMode.None;
        options.HttpOnly = HttpOnlyPolicy.None;
        options.Secure = CookieSecurePolicy.Always;
    });

    services.AddSingleton<AuthService>();
}

void Configure(WebApplication app, IConfiguration configuration)
{
    app.UseStaticFiles();
    app.UseCookiePolicy();

    app.UseAuthentication();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
    }

    app.UseRouting();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
    });
}