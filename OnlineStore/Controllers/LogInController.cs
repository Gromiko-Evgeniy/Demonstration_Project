using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using OnlineStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace OnlineStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private ILogInService logInService;

        public LogInController(ILogInService logInService)
        {
            this.logInService = logInService;
        }

        [HttpPost] 
        public async Task<IActionResult> LogIn(LogInDto userData)
        {
            var response = await logInService.GetUser(userData);
            if (response.Data is not null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, response.Data.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, response.Data.Role.ToString())

                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new ResponseInfo<object>(null, true, "logged out successfully"));
        }

        [Route("AccessDenied")]
        [HttpGet]
        //работает при ограничении на админа, но не при ограничении на авторизацию
        public IActionResult AccessDenied()
        {
            return Ok(new ResponseInfo<object>(null, false, "Access denied"));
        }

        //cru - create / read / update
    }
}
