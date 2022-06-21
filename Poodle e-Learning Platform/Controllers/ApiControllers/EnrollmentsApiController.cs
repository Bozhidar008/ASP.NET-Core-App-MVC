using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poodle_e_Learning_Platform.Services.Contracts;

namespace Poodle_e_Learning_Platform.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsApiController : ControllerBase
    {
        private readonly IEnrollmentService enrollmentService;

        public EnrollmentsApiController(IEnrollmentService enrollmentService)
        {
            this.enrollmentService = enrollmentService;
        }

        [Route("IsEnrolled")]
        [HttpDelete]
        public IActionResult IsEnrolled(int userId, int courseId)
        {
            if (!enrollmentService.IsEnrolled(userId, courseId))
            {
                return StatusCode(405);
            }

            return Ok();
        }
    }
}
