using BiocaLabs.Common.Exceptions;
using Lab.Application.DTOs;
using Lab.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace BiocaLabs.API.Controllers.Lab;

[ApiController]
[Route("api/[controller]")]
public class MedicineController(CreateMedicine createMedicineUc, GetMedicineByName getMedicineByNameUc) : ControllerBase
{
    [HttpGet("{name}")]
    public async Task<ActionResult<GetMedicineByNameOutput>> GetMedicineByName(string name)
    {
        try
        {
            var result = await getMedicineByNameUc.ExecuteAsync(name);

            if (result is null) return NotFound($"Medicine with name '{name}' not found.");

            return Ok(result);
        }
        catch (Exception e)
        {
            return e is DomainValidationException or StringValidationException
                ? BadRequest(e.Message)
                : StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

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
            return e is DomainValidationException or StringValidationException
                ? BadRequest(e.Message)
                : StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}