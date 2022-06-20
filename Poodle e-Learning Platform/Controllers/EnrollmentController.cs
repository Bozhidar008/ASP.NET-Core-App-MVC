using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poodle_e_Learning_Platform.Data;
using Poodle_e_Learning_Platform.Models;
using Poodle_e_Learning_Platform.Services.Contracts;
using Poodle_e_Learning_Platform.ViewModels.Course;
using System;
using System.Linq;
using System.Security.Claims;

namespace Poodle_e_Learning_Platform.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IEnrollmentService enrollmentService;
        private readonly ICoursesService coursesService;

        public EnrollmentController(IEnrollmentService enrollmentService, ICoursesService coursesService, ApplicationDbContext context)
        {
            this.enrollmentService = enrollmentService;
            this.coursesService = coursesService;
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetEnroll(int courseId)
        {
            EnrollViewModel viewModel = coursesService.GetAllStudents(courseId);

            return View("Enroll", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult Enroll(int userId, int courseId)
        {
            if (enrollmentService.IsEnrolled(userId, courseId))
            {
                ModelState.AddModelError("IsEnrolled", "Current user is already inrolled in this course!");
            }

            if (!ModelState.IsValid)
            {
                EnrollViewModel viewModel = coursesService.GetAllStudents(courseId);
                return View(viewModel);
            }

            enrollmentService.Create(userId, courseId);
            return RedirectToAction(nameof(GetEnroll), "Enrollment", new { courseId = courseId });
        }


        [HttpDelete]
        [Authorize(Roles = "Teacher")]
        public IActionResult Unenroll(int userId, int courseId)
        {
            enrollmentService.Delete(userId, courseId);

            return RedirectToAction("GetEnroll", new { courseId = courseId });            
        }

        [Authorize]
        public IActionResult UnenrollStudent(int userId, int courseId) 
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (currentUserId != userId)
            {
                return View("Error", new ErrorViewModel() { RequestId = "Not allowed!" });
            }

            enrollmentService.Delete(userId, courseId);

            return RedirectToAction("Index","Courses");

        }


    }
}
