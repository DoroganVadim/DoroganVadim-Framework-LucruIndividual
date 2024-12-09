using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LucruIndividual.Models.DbEntities;
using System.Security.Cryptography;
using System.Text;
using LucruIndividual.DataLayer;
using Microsoft.AspNetCore.Authorization;
using LucruIndividual.Models.Account;

namespace LucruIndividual.Controllers
{
    public class AccountController : Controller
    {
        protected SocialPlatformContext context;
        public AccountController(SocialPlatformContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (context.users.FirstOrDefault(u => u.email == model.email) != null)
                {
                    ModelState.AddModelError("login", "Un utilizator cu acest login exista deja.");
                    return View("Register", model);
                }
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    model.password = BitConverter.ToString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(model.password))).Replace("-", "").ToLower();
                }
                User user = new User { email = model.email, password = model.password, status = true};
                Profile profile = new Profile { user = user, name = model.name, surrname = model.surrname, birthDay = model.birthDay, sex = (model.sex == "F"? 0 : 1) };
                context.users.Add(user);
                context.profiles.Add(profile);
                context.SaveChanges();
                ModelState.Clear();
                return View("Login");
            }
            return View("Register", model);
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User model)
        {
            if (ModelState.IsValid)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    model.password = BitConverter.ToString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(model.password))).Replace("-", "").ToLower();
                }
                var dbUser = context.users.FirstOrDefault(u => u.email == model.email && u.password == model.password);
                if(dbUser.status == false)
                {
                    return RedirectToAction("Banned");
                }
                if (dbUser == null)
                {
                    ModelState.AddModelError("login", "Nu a fost gasit utilizatorul");
                    return View(model);
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, dbUser.email),
                        new Claim(ClaimTypes.Role, dbUser.role == 0 ? "User" : "Admin"),
                    };
                    ModelState.Clear();
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Login", model);
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Account");
        }

        public IActionResult Banned()
        {
            return View();
        }
    }
}
