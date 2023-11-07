using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using website.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using website.Data;
using website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic.CompilerServices;
using NuGet.Packaging.Signing;

namespace website.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Blog
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var vm = new PostPartialModel
            {
                Users = _context.Users.OrderByDescending(a => a.Id).ToList(),
                Posts = _context.Blogposts.OrderByDescending(a => a.Id).ToList()
            };

            return View(vm);
            /*
            var applicationDbContext = _context.Blogposts.Include(b => b.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
            */
        }

        // GET: Blog/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Blogposts == null)
            {
                return NotFound();
            }

            var blogpost = await _context.Blogposts
                .Include(b => b.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogpost == null)
            {
                return NotFound();
            }

            return View(blogpost);
        }

        // GET: Blog/Create
        [Authorize]
        public IActionResult Add()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,Title,Summary,Content")] Blogpost blogpost)
        {
            // Add user to the model
            if (User.Identity is {IsAuthenticated: true})
            {
                var user = _userManager.GetUserAsync(User).Result;
                blogpost.ApplicationUser = user;
                blogpost.Time = DateTime.Now.Date.ToString("dd.MM.yyyy");
            }

            ModelState.Clear();
            TryValidateModel(ModelState);
            
            if (ModelState.IsValid)
            {
                _context.Add(blogpost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", blogpost.ApplicationUserId);
            return View(blogpost);
        }

        // GET: Blog/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (id == null || _context.Blogposts == null)
            {
                return NotFound();
            }
            
            var blogpost = await _context.Blogposts.FindAsync(id);
            if (blogpost == null)
            {
                return NotFound();
            }
            if (blogpost.ApplicationUserId != user.Id) return Redirect("/Blog/Index");
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", blogpost.ApplicationUserId);
            return View(blogpost);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Summary,Content")] Blogpost blogpost)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (id != blogpost.Id)
            {
                return NotFound();
            }
            
            ModelState.Clear();
            TryValidateModel(ModelState);
            
            if (ModelState.IsValid)
            {
                _context.Update(blogpost);
                if (User.Identity is {IsAuthenticated: true})
                {
                    blogpost.ApplicationUser = user;
                    blogpost.Time = DateTime.Now.Date.ToString("dd.MM.yyyy");
                }
                
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(blogpost);
        }

        // GET: Blog/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Blogposts == null)
            {
                return NotFound();
            }

            var blogpost = await _context.Blogposts
                .Include(b => b.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (blogpost == null)
            {
                return NotFound();
            }

            return View(blogpost);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Blogposts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Blogposts'  is null.");
            }
            var blogpost = await _context.Blogposts.FindAsync(id);
            if (blogpost != null)
            {
                _context.Blogposts.Remove(blogpost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}