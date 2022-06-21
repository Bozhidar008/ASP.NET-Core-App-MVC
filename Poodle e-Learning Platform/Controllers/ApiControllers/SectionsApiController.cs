using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poodle_e_Learning_Platform.InputModels.Section;
using Poodle_e_Learning_Platform.Models;
using Poodle_e_Learning_Platform.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poodle_e_Learning_Platform.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsApiController : ControllerBase
    {
        private readonly ISectionsService sectionsService;
        private readonly ICoursesService coursesService;


        public SectionsApiController(ISectionsService sectionsService, ICoursesService coursesService)
        {
            this.sectionsService = sectionsService;
            this.coursesService = coursesService;
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            SectionPage section = sectionsService.GetSectionInfo(id);
            if (section == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, section);
        }


        [HttpPost]
        public IActionResult Create(SectionInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);


            }

            if (sectionsService.Exists(inputModel.Title))
            {
                return StatusCode(StatusCodes.Status400BadRequest);

            }



            sectionsService.Create(inputModel);

            return StatusCode(StatusCodes.Status201Created, inputModel);
        }

        [HttpPut]
        public IActionResult Edit(EditSectionInputModel inputModel)
        {


            sectionsService.Update(inputModel);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int courseId = sectionsService.Delete(id);

            return StatusCode(StatusCodes.Status200OK);
        }


       
    }
}
