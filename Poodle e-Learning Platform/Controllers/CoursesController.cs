using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Poodle_e_Learning_Platform.Data;
using Poodle_e_Learning_Platform.Globals;
using Poodle_e_Learning_Platform.InputModels.Course;
using Poodle_e_Learning_Platform.Models;
using Poodle_e_Learning_Platform.Services.Contracts;
using Poodle_e_Learning_Platform.ViewModels.Course;
using Poodle_e_Learning_Platform.ViewModels.Home;
using Poodle_e_Learning_Platform.ViewModels.Student;

namespace Poodle_e_Learning_Platform.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICoursesService coursesService;
        private readonly IEnrollmentService enrollmentService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationUserService applicationUserService;

        public CoursesController(ApplicationDbContext context, 
            ICoursesService coursesService, 
            IEnrollmentService enrollmentService, 
            IApplicationUserService applicationUserService,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
            this.coursesService = coursesService;
            this.enrollmentService = enrollmentService;
            this.applicationUserService = applicationUserService;
        }

        // GET: Courses
        [Authorize]
        public async Task<IActionResult> Index(int page, string orderBy = "")
        {
            ApplicationUser current = userManager.Users.FirstOrDefault(u => u.Id == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (current is not null && await userManager.IsInRoleAsync(current, "Student")) 
            {
                return RedirectToAction(nameof(StudentCourses), new { studentId = current.Id });
            }

            var applicationDbContext = _context.Courses.Select(c => c);//.Include(c => c.ApplicationUser);


            IEnumerable<Course> courses = await applicationDbContext.ToListAsync();

            if (!string.IsNullOrEmpty(orderBy)) 
            {
                if (orderBy.ToLower() == "enddate")
                {
                    courses = await _context.Courses.Select(c => c).OrderBy(c => c.EndDate).ToListAsync();
                }

                if (orderBy.ToLower() == "startdate")
                {
                    courses = await _context.Courses.Select(c => c).OrderBy(c => c.StartDate).ToListAsync();
                }

                if (orderBy.ToLower() == "created")
                {
                    courses = await _context.Courses.Select(c => c).OrderBy(c => c.Created).ToListAsync();
                }

                if (orderBy.ToLower() == "title") 
                {
                    courses = await _context.Courses.Select(c => c).OrderBy(c => c.Title).ToListAsync();
                }

                if (orderBy.ToLower() == "description")
                {
                    courses = await _context.Courses.Select(c => c).OrderBy(c => c.Description).ToListAsync();
                }

                if (orderBy.ToLower() == "isprivate")
                {
                    courses = await _context.Courses.Select(c => c).OrderBy(c => c.IsPrivate).ToListAsync();
                }
            }
            return View(courses);
        }

        // GET: Courses/Details/5
        [Authorize]
        public IActionResult Details(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (coursesService.IsPrivate(id))
            {
                if (!User.IsInRole("Teacher") && !enrollmentService.IsEnrolled(userId, id))
                {
                    return View("Error", new ErrorViewModel() { RequestId = "Access denied!" });
                }
            }


            if (!enrollmentService.IsEnrolled(userId, id) && !User.IsInRole("Teacher"))
            {
                enrollmentService.Create(userId, id);
            }


            DetailsViewModel course = coursesService.GetCourseDetails(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create

        [Authorize(Roles = "Teacher")]
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: Courses/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public IActionResult Create(CourseInputModel inputModel)
        {

            if (coursesService.Exists(inputModel.Title))
            {
                ModelState.AddModelError("Title", "There is already a course with the spacified name");
            }

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            coursesService.Create(inputModel);

            return RedirectToAction(nameof(Index));
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "FirstName", course.ApplicationUserId);
            return View(course);
        }

        // POST: Courses/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ApplicationUserId,IsPrivate,IsStudentEnrolled,Created,StartDate,EndDate")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "FirstName", course.ApplicationUserId);
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Teacher")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            coursesService.Delete((int)id);


            return RedirectToAction("index", "Home");
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        [Authorize]
        public async Task<IActionResult> StudentCourses(int studentId, int page = 1) 
        {
            var user = userManager.Users.FirstOrDefault(u => u.Id == studentId);

            if (user is not null && await userManager.IsInRoleAsync(user, "Teacher")) 
            {
                return RedirectToAction(nameof(Index));
            }

            IndexViewModel viewModel = coursesService
                .GetStudentIndexPage(studentId, page, User.Identity.IsAuthenticated);
            return View(viewModel);
        }
    }
}
