using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poodle_e_Learning_Platform.Data;
using Poodle_e_Learning_Platform.InputModels.ApplicationUser;
using Poodle_e_Learning_Platform.Models;
using Poodle_e_Learning_Platform.Services.Contracts;
using Poodle_e_Learning_Platform.ViewModels.ApplicationUsers;

namespace Poodle_e_Learning_Platform.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IApplicationUserService userService;

        public ApplicationUsersController(ApplicationDbContext context, IApplicationUserService userService)
        {
            _context = context;
            this.userService = userService;
        }

        // GET: ApplicationUsers
        [Authorize(Roles = "Teacher")]
        public IActionResult Index()
        {
            IEnumerable<ApplicationUserViewModel> model = userService.GetAllUsers();

            return View(model);           
        }

        // GET: ApplicationUsers/Details/5
        [Authorize]
        public IActionResult Details(int id)
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (currentUserId != id)
            {
                ErrorViewModel error = new ErrorViewModel()
                {
                    RequestId = "Unauthorized",
                };
                return View("Error", error);
            }

            ApplicationUserViewModel model = userService.GetUserDetails(id);

            return View(model);
        }

        // GET: ApplicationUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUsers/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,ProfilePicture,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Edit/5
        public IActionResult Edit(int id)
        {
            var applicationUser = userService.ApplicationUserData(id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationUserInputModel inputModel)
        {
            if (!ModelState.IsValid) 
            {
                return View(inputModel);
            }

            userService.EditUser(inputModel);

            if (userService.IsTeacher(inputModel.Id))
            {
                return RedirectToAction(nameof(Index));
            };

            return RedirectToAction(nameof(Details), new { id = inputModel.Id });
        }

        // GET: ApplicationUsers/Delete/5
        public IActionResult Delete(int id)
        {
             userService.Delete(id);

            return  RedirectToAction(nameof(Index));
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
