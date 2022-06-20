using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Poodle_e_Learning_Platform.Data;
using Poodle_e_Learning_Platform.InputModels.Course;
using Poodle_e_Learning_Platform.Models;
using Poodle_e_Learning_Platform.Services.Contracts;
using Poodle_e_Learning_Platform.ViewModels.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poodle_e_Learning_Platform.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Teacher")]
    public class CoursesApiController : ControllerBase
    {


        private readonly ApplicationDbContext _context;
        private readonly ICoursesService coursesService;
        private readonly IApplicationUserService applicationUserService;
        private readonly IEnrollmentService enrollmentService;
        private readonly IApplicationUserService userService;

        public object ViewData { get; private set; }

        public CoursesApiController(ApplicationDbContext context, ICoursesService coursesService, IApplicationUserService applicationUserService, IEnrollmentService enrollmentService, IApplicationUserService userService)
        {
            _context = context;
            this.coursesService = coursesService;
            this.applicationUserService = applicationUserService;
            this.enrollmentService = enrollmentService;
            this.userService = userService;
        }




        // POST: Courses/Create

        [HttpGet]
        public IActionResult Get()
        {
            var course = new List<Course>();
            course = coursesService.Get();
            return StatusCode(StatusCodes.Status200OK, course);

        }


        [HttpPost]
        public IActionResult Create(CourseInputModel inputModel)
        {
            coursesService.Create(inputModel);

            return this.StatusCode(StatusCodes.Status200OK, inputModel);

        }

        [HttpPut("{id}")]

        public IActionResult Update(CourseInputModel inputModel)
        {



            coursesService.Create(inputModel);

            return this.StatusCode(StatusCodes.Status200OK, inputModel);
        }

        // GET: Courses/Edit/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Edit(int? id)
        //{

        //    return this.StatusCode(StatusCodes.Status200OK, course);
        //}

        // POST: Courses/Edit/5




        // GET: Courses/Delete/5

        [HttpDelete("{id}")]


        public IActionResult Delete(int id)
        {
          

           this.coursesService.Delete(id);
          

            return StatusCode(StatusCodes.Status200OK);

        }

  
       

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        
    }
}
