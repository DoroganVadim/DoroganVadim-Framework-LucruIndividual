using LucruIndividual.DataLayer;
using LucruIndividual.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LucruIndividual.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        protected SocialPlatformContext context;
        public AdminController(SocialPlatformContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;

            var profiles = context.profiles.Include(p => p.user).Include(p => p.profileImage).Where(p => p.user.role != 1).ToList();

            var pagedProfiles = profiles.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var totalPages = (int)Math.Ceiling((double)profiles.Count / pageSize);

            AdminPanelModel model = new AdminPanelModel
            {
                profiles = pagedProfiles,
                pageNumber = pageNumber,
                totalPages = totalPages,
                pageSize = pageSize
            };

            return View(model);
        }

        public IActionResult ChangeStatus(int? page, int id)
        {
            var dbProfile = context.profiles.Include(p => p.user).FirstOrDefault(p=>p.userId == id);
            if(dbProfile == null)
            {
                return View("Index");
            }
            dbProfile.user.status = (dbProfile.user.status? false : true);
            context.SaveChanges();
            return RedirectToAction("Index", new { page = page });
        }
    }
}
