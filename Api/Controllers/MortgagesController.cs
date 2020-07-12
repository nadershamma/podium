using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Applicant;
using Api.Application.Mortgage;
using Api.Core.Applicant;
using Api.Core.Mortgage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MortgagesController : ControllerBase
    {
        private readonly IMortgageService _service;

        public MortgagesController(IMortgageService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all mortgages.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get /mortgages
        /// 
        /// </remarks>
        /// <returns> List of mortgages in the database</returns>
        /// <response code="200">Returns list of mortgages</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Mortgage>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Mortgage>>> GetMortgages(long? applicantId, decimal? propertyValue,
            decimal? depositValue)
        {
            IEnumerable<Mortgage> mortgages;
            if (applicantId != null && propertyValue != null & depositValue != null)
            {
                mortgages = await _service.GetQualifiedMortgages((long) applicantId, (decimal) propertyValue,
                    (decimal) depositValue);
            }
            else
            {
                mortgages = await _service.GetMortgages();
            }

            return Ok(mortgages);
        }


        /// <summary>
        /// Get mortgage by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get /mortgages/{id}
        /// 
        /// </remarks>
        /// <returns> Mortgage details in the database</returns>
        /// <response code="200">Return mortgage</response>
        /// <response code="404">Mortgage not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Mortgage), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Mortgage>> GetMortgage(long id)
        {
            var mortgage = await _service.GetMortgage(id);

            if (mortgage == null)
            {
                return NotFound();
            }

            return mortgage;
        }

        /// <summary>
        /// Creates a new mortgage.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /mortgages
        ///     {
        ///        "Lender": "Lender",
        ///        "Rate": "4",
        ///        "Type": "Fixed",
        ///        "LoanToValue": "70"
        ///     }
        /// 
        /// </remarks>
        /// <param name="createMortgageDto"></param>
        /// <returns> Id of the newly created Applicant</returns>
        /// <response code="201">Returns 201 Created</response>
        /// <response code="400">If mortgage is null</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateMortgageDto>> PostApplicant(CreateMortgageDto createMortgageDto)
        {
            var mortgageId = await _service.CreateMortgage(createMortgageDto);

            return CreatedAtAction(
                nameof(GetMortgage),
                new {id = mortgageId},
                new {id = mortgageId});
        }

        /// <summary>
        /// Delete an mortgage.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Delete /mortgages/{id}
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <returns> 204 no content</returns>
        /// <response code="204">Returns OK no content</response>
        /// <response code="404">If the applicant is null</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteMortgage(long id)
        {
            if (!_service.MortgageExists(id))
            {
                return NotFound();
            }

            await _service.DeleteMortgage(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}