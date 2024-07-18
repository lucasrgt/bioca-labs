using BiocaLabs.Common.Exceptions;
using Lab.Application.DTOs;
using Lab.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace BiocaLabs.API.Controllers.Lab;

[ApiController]
[Route("api/[controller]")]
public class MedicineController(CreateMedicine createMedicineUc) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateMedicineOutput>> CreateMedicine([FromBody] CreateMedicineInput body)
    {
        try
        {
            var result = await createMedicineUc.ExecuteAsync(body);
            return Ok(result);
        }
        catch (Exception e)
        {
            if (e is DomainValidationException or StringValidationException) return BadRequest(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}