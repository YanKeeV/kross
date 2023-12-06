using SampleMvcApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SampleMvcApp;

public sealed class AuthService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMemoryCache _cache;

    public AuthService(IHttpContextAccessor contextAccessor, IMemoryCache cache) => (_contextAccessor, _cache) = (contextAccessor, cache);

    public bool Register(UserModel model)
    {
        var result = _cache.Set<UserModel>(GetCacheKey(model.emailAddress), model);

        if (result is not null)
            return true;

        return false;
    }

    public async Task Logout()
    {
        await _contextAccessor.HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public bool Login(UserModel model)
    {
        var result = _cache.Get<UserModel>(GetCacheKey(model.emailAddress));

        if (result is not null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.name),
                new Claim(ClaimTypes.Email, result.emailAddress),

            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            _contextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties).Wait();

            return true;
        }

        return false;
    }

    private string GetCacheKey(string emailAddress)
        => $"user_{emailAddress}";
}
