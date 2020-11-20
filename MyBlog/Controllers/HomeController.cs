using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyBlog.Data;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyBlogContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public HomeController(ILogger<HomeController> logger, MyBlogContext context, UserManager<IdentityUser> userManager,
                                   SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                var myBlogContext = _context.BlogPosts.Include(b => b.User);
                var allblogs = await myBlogContext.ToListAsync();
                var varmail = User.Identity.Name;
                var currentuser = _context.User.SingleOrDefault(x => x.Email == varmail);
                var friends = await _context.Followings.ToListAsync();

                List<int> myfrnds = new List<int>();
                List<BlogPost> myblogs = new List<BlogPost>();
                foreach (var mf in friends)
                {
                    if (currentuser.UserId == mf.UserId)
                    {
                        var frnd = _context.User.SingleOrDefault(x => x.UserId == mf.FollowsId);
                        myfrnds.Add(frnd.UserId);
                    }

                }

                foreach (var post in allblogs)
                {
                    if (myfrnds.Contains(post.UserId))
                    {
                        myblogs.Add(post);
                    }
                }
                return View(myblogs);
            }
            else
            {
                return View();
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
