using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBlog.Data;
using MyBlog.Models;

namespace MyBlog.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly MyBlogContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public AccountController(UserManager<IdentityUser> userManager,
                                   SignInManager<IdentityUser> signInManager, MyBlogContext context, IWebHostEnvironment hostEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(User model)
        {


            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };


                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(model.ImagePath.FileName);
                string extension = Path.GetExtension(model.ImagePath.FileName);
                model.ImagePathName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                Console.WriteLine(fileName);

                string path = Path.Combine(wwwRootPath + "/Profile/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await model.ImagePath.CopyToAsync(fileStream);
                }



                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);

                    _context.Add(model);
                    await _context.SaveChangesAsync();
                   
                    return RedirectToAction("index", "home");
                }

                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {


            if (ModelState.IsValid)
            {

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                   

                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError("", "Invalid Login Attempt!");
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("login", "account");
        }

    }
}
