using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TUSO.Domain.Dto;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

namespace TUSO.Web.Controllers
{
    /*
     * Created by: Rakib
     * Date created: 11.09.2022
     * Last modified: 11.09.2022
     * Modified by: Rakib
     */
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client;
        public HomeController(ILogger<HomeController> logger, HttpClient client)
        {
            _logger = logger;
            this.client = client;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Login")]
        //[AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                return RedirectUserToRightPage(role);
            }

            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            UserAccount user = await new HomeHttpClient(client).UserLogin(model);    
            if (user.Username != null)
            {
                var claims = SetClaims(user);
                var userIdentity = new ClaimsIdentity(claims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                        IsPersistent = true,
                        AllowRefresh = true
                    });
                return RedirectUserToRightPage("Add");
            }
            ViewData["msg"] = "Invalid Email or Password";
            return View(model);
        }

        [HttpGet("Logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        private IActionResult RedirectUserToRightPage(string role)
        {
            switch (role)
            {
                case "Admin":
                    return Redirect("/Home");
                case "Client":
                    return Redirect("/Home");
                case "Agent":               
                    return Redirect("/Home");
                default:
                    return Redirect("/Forbidden");
            }
        }

        private List<Claim> SetClaims(UserAccount user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Sid,user.OID.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email, user.Username),
                //new Claim(ClaimTypes.Role, user.Roles.RoleName)
            };
        }

    }
}