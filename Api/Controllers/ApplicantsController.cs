using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Applicant;
using Api.Core.Applicant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private readonly IApplicantService _service;
        
        public ApplicantsController(IApplicantService service)
        {
            _service = service;
        }
        
        /// <summary>
        /// Get all applicants.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get /applicants
        /// 
        /// </remarks>
        /// <returns> List of applicants in the database</returns>
        /// <response code="200">Returns list of applicants</response>
        [HttpGet]
        [ProducesResponseType( typeof(IEnumerable<Applicant>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Applicant>>> GetApplicants()
        {
            var applicants = await _service.GetApplicants();
            return Ok(applicants);
        }

        /// <summary>
        /// Get applicant by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get /applicants/{id}
        /// 
        /// </remarks>
        /// <returns> Applicant details in the database</returns>
        /// <response code="200">Return applicant</response>
        /// <response code="404">Applicant not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType( typeof(Applicant), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Applicant>> GetApplicant(long id)
        {
            var applicant = await _service.GetApplicant(id);

            if (applicant == null)
            {
                return NotFound();
            }

            return applicant;
        }


        /// <summary>
        /// Creates a new applicant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /applicants
        ///     {
        ///        "FirstName": "Joe",
        ///        "LastName": "Bloggs",
        ///        "DateOfBirth": "1970-05-09",
        ///        "Email": "joebloggs@example.com"
        ///     }
        /// 
        /// </remarks>
        /// <param name="createApplicantDto"></param>
        /// <returns> Id of the newly created Applicant</returns>
        /// <response code="201">Returns the newly created applicant id</response>
        /// <response code="400">If the applicant is null</response> 
        [HttpPost]
        [ProducesResponseType( typeof(long), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateApplicantDto>> PostApplicant(CreateApplicantDto createApplicantDto)
        {
            var applicantId = await _service.CreateApplicant(createApplicantDto);
            
            return CreatedAtAction(
                nameof(GetApplicant), 
                new {id = applicantId}, 
                new {id = applicantId});
        }
        
        /// <summary>
        /// Delete an applicant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Delete /applicants/{id}
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <returns> Id of the newly created Applicant</returns>
        /// <response code="204">Returns OK no content</response>
        /// <response code="404">If the applicant is null</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType( statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType( StatusCodes.Status404NotFound)] 
        public async Task<ActionResult> DeleteApplicant(long id)
        {
            if (!_service.ApplicantExists(id))
            {
                return NotFound();
            }

            await _service.DeleteApplicant(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
        
    }
}