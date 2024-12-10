using LucruIndividual.DataLayer;
using LucruIndividual.Models;
using LucruIndividual.Models.DbEntities;
using LucruIndividual.Models.Home;
using LucruIndividual.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LucruIndividual.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SocialPlatformContext context;
        private readonly FriendRequestCountMiddleware count;

        public HomeController(FriendRequestCountMiddleware count,ILogger<HomeController> logger, SocialPlatformContext context)
        {
            this.context = context;
            _logger = logger;
            this.count = count;
        }
        private HomePageModel createHomePageModel(PostInformation? post = null)
        {
            HomePageModel model = new HomePageModel();
            model.post = post;
            model.otherPosts = context.posts.Include(po => po.profile).ThenInclude(pr=>pr.profileImage).Include(po=>po.image).Where(po => po.status == true).OrderByDescending(po => po.createdTime).ToList();
            return model;
        }

        public void changeFriendRequestNumber()
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            count.value = context.friendRelations.Include(f => f.user1).Include(f => f.user2).Count(f => f.user2.email == email && f.status == false);
        }

        public IActionResult Index()
        {
            changeFriendRequestNumber();
            return View(createHomePageModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(HomePageModel homePageModel, IFormFile? image)
        {
            PostInformation model = homePageModel.post;
            if (ModelState.IsValid){
                var email = User.FindFirst(ClaimTypes.Name)?.Value;
               
                var post = new Post
                {
                    postText = model.postText,
                    status = model.status,
                };
                post.profile = context.profiles.First(u => u.user.email == email);
                if (image != null && image.Length > 0)
                {
                    post.image = new PostImage(Path.GetExtension(image.FileName), "post");
                }

                await context.posts.AddAsync(post);
                await context.SaveChangesAsync();

                if (image != null && image.Length > 0)
                {
                    var fileName = $"{post.image.category}{post.image.id}{post.image.extension}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    var directoryPath = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                }
                return RedirectToAction("Index");
            }
            return View("Index",createHomePageModel(model));
        }

        public IActionResult Profile(int? id = null)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            var profile = context.profiles.Include(p=>p.user).Where(p=>p.user.email == email).FirstOrDefault();
            ProfileInformation pI = new ProfileInformation();
            pI.friendCheck = 0;
            if (id == profile.userId || id == null)
            {
                pI.profile = context.profiles.Include(p => p.profileImage).Include(p => p.user).First(p=>p.user.email == email);
                pI.isTheUser = true;
                pI.friendCheck = 1;
                pI.posts = context.posts.Include(p => p.profile).Include(p => p.image).Where(p => p.profile.userId == pI.profile.userId).ToList();
            }
            else
            {
                pI.profile = context.profiles.Include(p => p.profileImage).Include(p => p.user).First(p => p.userId == id);
                pI.isTheUser = false;
                if (context.friendRelations.Where(fr => fr.user1.id == profile.user.id && fr.user2.id == pI.profile.user.id && fr.status == true).ToList().Count == 1) pI.friendCheck = 2;
                if (context.friendRelations.Where(fr => fr.user1.id == profile.user.id && fr.user2.id == pI.profile.user.id && fr.status == false).ToList().Count == 1) pI.friendCheck = 1;
                pI.posts = context.posts.Include(p => p.profile).Include(p => p.image).Where(p => p.profile.userId == pI.profile.userId && p.status == true).ToList();
            }
           
            return View(pI);
        }

        public IActionResult EditProfile()
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            var profile = context.profiles.Include(p=>p.user).Include(p=>p.profileImage).FirstOrDefault(p=>p.user.email == email);
            EditProfileModel editProfile = new EditProfileModel
            {
                profileImage = profile.profileImage,
                birthDay = profile.birthDay,
                aboutUser = profile.aboutUser,
                nickname = profile.nickname,
                name = profile.name,
                surrname = profile.surrname,
                sex = (profile.sex == 0 ? "F" : "M"),
                phoneNumber = profile.phoneNumber
            };
            return View(editProfile);
        }

        public IActionResult DeletePost(int? postId = null)
        {
            var post = context.posts.First(fr => fr.id == postId);
            context.posts.Remove(post);
            context.SaveChanges();
            return RedirectToAction("Profile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProfile(EditProfileModel model, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                var email = User.FindFirst(ClaimTypes.Name)?.Value;
                var dbProfile = context.profiles.Include(p => p.user).Include(p => p.profileImage).FirstOrDefault(p => p.user.email == email);
                dbProfile.name = model.name;
                dbProfile.surrname = model.surrname;
                dbProfile.birthDay = model.birthDay;
                dbProfile.sex = (model.sex == "F" ? 0 : 1);
                dbProfile.phoneNumber = model.phoneNumber;
                dbProfile.aboutUser = model.aboutUser;
                dbProfile.nickname = model.nickname;
                if (dbProfile.profileImage == null && image != null && image.Length > 0)
                {
                    dbProfile.profileImage = new ProfileImage(Path.GetExtension(image.FileName), "profile");
                }
                else if (image != null && image.Length > 0)
                {
                    var fileName = $"{dbProfile.profileImage.category}{dbProfile.profileImage.id}{dbProfile.profileImage.extension}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    ImageFileManagement.deleteImage(filePath);
                    dbProfile.profileImage.extension = Path.GetExtension(image.FileName);
                }

                context.SaveChanges();
                ModelState.Clear();

                if (image != null && image.Length > 0)
                {
                    var fileName = $"{dbProfile.profileImage.category}{dbProfile.profileImage.id}{dbProfile.profileImage.extension}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    var directoryPath = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyToAsync(fileStream);
                    }
                }

                
                return RedirectToAction("Profile");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendFriendRequest(int id_friend, string action)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            User? user1 = context.users.FirstOrDefault(u=>u.email == email);
            User? user2 = context.users.FirstOrDefault((u)=>u.id == id_friend);
            if(user1 == null || user2 == null)
            {
                return RedirectToAction("Index");
            }
            FriendRelation? dbRelation = context.friendRelations.Include(c => c.user1).Include(c => c.user2).FirstOrDefault(c => c.user1 == user1 && c.user2 == user2);
            FriendRelation relation = new FriendRelation();
            if ( dbRelation == null) {
                relation.user1 = user1;
                relation.user2 = user2;
                relation.status = false;
                context.friendRelations.Add(relation);
            }
            dbRelation = context.friendRelations.Include(c => c.user1).Include(c => c.user2).FirstOrDefault(c => c.user1 == user2 && c.user2 == user1 && c.status == false);
            if (dbRelation != null)
            {
                relation.status = true;
                dbRelation.status = true;
                context.friendRelations.Add(relation);
            }
            context.SaveChanges();
            changeFriendRequestNumber();
            return RedirectToAction(action,new { id = id_friend });
        }

        public IActionResult FriendRequests(int? id = null)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            FriendsInformationModel model = new FriendsInformationModel();
            model.isUser = true;
            model.profiles = context.profiles.Include(p => p.user).Include(p => p.profileImage).Where(p => context.friendRelations.Any(fr => fr.user1Id == p.userId && fr.status == false && p.user.email != email)).ToList();
            return View(model);
        }

        public IActionResult DeleteFriendRequest(int? userId = null)
        {
            var friendRequest = context.friendRelations.First(fr=>fr.user1Id == userId && fr.status == false);
            context.friendRelations.Remove(friendRequest);
            context.SaveChanges();
            changeFriendRequestNumber();
            return RedirectToAction("FriendRequests");
        }

        public IActionResult FriendList(int? id = null)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            FriendsInformationModel model = new FriendsInformationModel();
            model.isUser = true;
            model.profiles = context.profiles.Include(p => p.user).Include(p => p.profileImage).Where(p => context.friendRelations.Any(fr => fr.user1Id == p.userId && fr.status == true && p.user.email != email)).ToList();
            return View(model);
        }

        public IActionResult DeleteFriend(int id_friend, string action)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            User? user1 = context.users.FirstOrDefault(u => u.email == email);
            User? user2 = context.users.FirstOrDefault((u) => u.id == id_friend);
            var friendRelation = context.friendRelations.Where(fr => (fr.user1 == user1 && fr.user2 == user2) || (fr.user1 == user2 && fr.user2 == user1) && fr.status == true).ToList();
            context.friendRelations.RemoveRange(friendRelation);
            context.SaveChanges();
            return RedirectToAction(action, new { id = id_friend });
        }

        public IActionResult SearchUser(string searchName)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            FriendsInformationModel model = new FriendsInformationModel();
            model.isUser = true;
            if (searchName != null)
            {
                string[] words = searchName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if(words.Length == 1)
                {
                    model.profiles = context.profiles.Include(p => p.user).Include(p => p.profileImage).Where(p => p.user.email != email && (p.name.StartsWith(words[0]) || p.surrname.StartsWith(words[0]))).ToList();
                }
                else if (words.Length >= 2)
                {
                    model.profiles = context.profiles.Include(p => p.user).Include(p => p.profileImage).Where(p => p.user.email != email && (p.name.StartsWith(words[0]) && p.surrname.StartsWith(words[1])) || (p.name.StartsWith(words[1]) && p.surrname.StartsWith(words[0]))).ToList();
                }
            }
            else
            {
                model.profiles = context.profiles.Include(p => p.user).Include(p => p.profileImage).Where(p => p.user.email != email).ToList();
            }
            return View(model);
        }
    }
}
