using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Poodle_e_Learning_Platform.InputModels.Section;
using Poodle_e_Learning_Platform.Models;
using Poodle_e_Learning_Platform.Services.Contracts;
using Poodle_e_Learning_Platform.ViewModels.Sections;
using System;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;

namespace Poodle_e_Learning_Platform.Controllers
{
    [Authorize]
    public class SectionsController : Controller
    {
        private readonly IEnrollmentService enrollmentService;
        private readonly ISectionsService sectionsService;
        private readonly IVideoServise videoServise;
        private readonly UserManager<ApplicationUser> userManager;

        public SectionsController(
            IEnrollmentService enrollmentService, 
            ISectionsService sectionsService, 
            IVideoServise videoServise,
            UserManager<ApplicationUser> userManager)
        {
            this.enrollmentService = enrollmentService;
            this.sectionsService = sectionsService;
            this.videoServise = videoServise;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Create(SectionInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Courses", new { id = inputModel.CourseId });
            }

            sectionsService.Create(inputModel);

            return RedirectToAction("Details", "Courses", new { id = inputModel.CourseId });
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult Delete(int id, string returnCourseId)
        {
            sectionsService.Delete(id);

            return RedirectToAction("Details", "Courses", new { id = returnCourseId });
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Edit(int id) 
        {
            EditSectionInputModel model = sectionsService.GetEditSectionData(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Edit(EditSectionInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            sectionsService.Update(inputModel);
            return RedirectToAction("Details", "Courses", new { id = inputModel.CourseId });
        }

        public async Task<IActionResult> Details(int id)
        {
            ApplicationUser user = userManager.Users.FirstOrDefault(u => u.Id == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            videoServise.GetAllVideos();
            if (await userManager.IsInRoleAsync(user, "Student"))
            {
                int courseId = sectionsService.GetCourseIdBySectionId(id);

                if (!enrollmentService.IsEnrolled(user.Id, courseId)) 
                {
                    return RedirectToAction("Index", "Courses");
                }
            }

            SectionViewModel model = sectionsService.GetSectionDetails(id);
            return View(model);
        }
    }
}
