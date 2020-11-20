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
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    [Authorize]
    public class BlogPostsController : Controller
    {
        private readonly MyBlogContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        public BlogPostsController(MyBlogContext context , UserManager<IdentityUser> userManager,
                                   SignInManager<IdentityUser> signInManager, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: BlogPosts
        public async Task<IActionResult> Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                var myBlogContext = _context.BlogPosts.Include(b => b.User);
                var allblogs = await myBlogContext.ToListAsync();
                var varmail = User.Identity.Name;
                var currentuser = _context.User.SingleOrDefault(x => x.Email == varmail);
                var friends = await _context.Followings.ToListAsync();
               
               
                List<BlogPost> myblogs = new List<BlogPost>();
               
                
                foreach(var post in allblogs)
                {
                   if(post.UserId == currentuser.UserId)
                    {
                        myblogs.Add(post);
                    }
                }
                return View(myblogs);
            }
            return View();
        }

        // GET: BlogPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BlogPostId == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        public async Task<IActionResult> ReadMore(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BlogPostId == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Contact");
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogPostId,Title,Content,ImagePath")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                var varmail = User.Identity.Name;
                var currentuser = _context.User.SingleOrDefault(x => x.Email == varmail);
                blogPost.UserId = currentuser.UserId;
                
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(blogPost.ImagePath.FileName);
                string extension = Path.GetExtension(blogPost.ImagePath.FileName);
                blogPost.ImagePathName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                Console.WriteLine(fileName);
               
                string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await blogPost.ImagePath.CopyToAsync(fileStream);
                }

                _context.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Contact", blogPost.UserId);
            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Contact", blogPost.UserId);
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogPostId,Title,Content,ImagePath,UserId")] BlogPost blogPost)
        {
            if (id != blogPost.BlogPostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                

                try
                {
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.BlogPostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Contact", blogPost.UserId);
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BlogPostId == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(int id)
        {
            return _context.BlogPosts.Any(e => e.BlogPostId == id);
        }
    }
}
