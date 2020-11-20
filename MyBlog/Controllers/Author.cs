using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    [Authorize]
    public class Author : Controller
    {
        private readonly MyBlogContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public Author(MyBlogContext context, UserManager<IdentityUser> userManager,
                                   SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                var allusers = await _context.User.ToListAsync();
                var varmail = User.Identity.Name;
                var tempuser = _context.User.SingleOrDefault(x => x.Email == varmail);
                var friends = await _context.Followings.ToListAsync();
                List<User> auths = new List<User>();
                List<User> myfrnds = new List<User>();
                foreach (var mf in friends)
                {
                    if(tempuser.UserId == mf.UserId)
                    {
                        var frnd = _context.User.SingleOrDefault(x => x.UserId == mf.FollowsId);
                        myfrnds.Add(frnd);
                    }
                
                }

                foreach(var usr in allusers)
                {
                    if (usr.Equals(tempuser))
                    {
                        Console.WriteLine("Do nothing");
                    }

                    else
                    {
                        if (!myfrnds.Contains(usr))
                        {
                            auths.Add(usr);
                        }
                    }
                }

              
                return View(auths);
            }
            return View();
        }

        public async Task<IActionResult> Follow(int? id)
        {
            Console.WriteLine("follow index");

            if (signInManager.IsSignedIn(User))
            {
                var allusers = await _context.User.ToListAsync();
                var varmail = User.Identity.Name;
                var currentuser = _context.User.SingleOrDefault(x => x.Email == varmail);
                var user = await _context.User.FirstOrDefaultAsync(m => m.UserId == id);
                Following follow = new Following();
                follow.UserId = currentuser.UserId;
                follow.FollowsId = user.UserId;
                if (ModelState.IsValid)
                {
                    _context.Add(follow);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Added");
                   
                }

                var friends = await _context.Followings.ToListAsync();
                List<User> fauths = new List<User>();
                foreach (var friend in friends)
                {
                   if(friend.UserId == currentuser.UserId)
                    {
                        var tobeadded = await _context.User.FirstOrDefaultAsync(m => m.UserId == friend.FollowsId);
                        fauths.Add(tobeadded);
                    }
                }

                
                return View(fauths);
            }

            return View();
        
        }

        public async Task<IActionResult> Unfollow(int? id)
        {
           

            if (signInManager.IsSignedIn(User))
            {
              //  var allusers = await _context.User.ToListAsync();
                var varmail = User.Identity.Name;
                var currentuser = _context.User.SingleOrDefault(x => x.Email == varmail);
                var user = await _context.User.FirstOrDefaultAsync(m => m.UserId == id);
                
                var friends = await _context.Followings.ToListAsync();
                
                foreach (var friend in friends)
                {
                    if (friend.UserId == currentuser.UserId && friend.FollowsId == id)
                    {
                        var removeid = friend.Id;
                        var unfollow = await _context.Followings.FindAsync(removeid);
                        _context.Followings.Remove(unfollow);
                        await _context.SaveChangesAsync();
                        break;

                    }
                }

                friends = await _context.Followings.ToListAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();

        }
        public async Task<IActionResult> Friends()
        {
           // Console.WriteLine("follow index");

            if (signInManager.IsSignedIn(User))
            {
                var allusers = await _context.User.ToListAsync();
                var varmail = User.Identity.Name;
                var currentuser = _context.User.SingleOrDefault(x => x.Email == varmail);
               // var user = await _context.User.FirstOrDefaultAsync(m => m.UserId == id);
                Following follow = new Following();
                follow.UserId = currentuser.UserId;
                //follow.FollowsId = user.UserId;
                
                var friends = await _context.Followings.ToListAsync();
                List<User> fauths = new List<User>();
                foreach (var friend in friends)
                {
                    if (friend.UserId == currentuser.UserId)
                    {
                        var tobeadded = await _context.User.FirstOrDefaultAsync(m => m.UserId == friend.FollowsId);
                        fauths.Add(tobeadded);
                    }
                }


                return View(fauths);
            }

            return View();
        }
    }
}
