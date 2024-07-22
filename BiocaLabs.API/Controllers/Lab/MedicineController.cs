using BiocaLabs.Common.Exceptions;
using Lab.Application.DTOs;
using Lab.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace BiocaLabs.API.Controllers.Lab;

[ApiController]
[Route("api/[controller]")]
public class MedicineController(
    CreateMedicine createMedicineUc,
    GetMedicineByName getMedicineByNameUc,
    UpdateMedicine updateMedicineUc,
    DeleteMedicine deleteMedicineUc) : ControllerBase
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
        catch (Exception ex)
        {
            return ex is DomainValidationException or StringValidationException
                ? BadRequest(ex.Message)
                : StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
        catch (Exception ex)
        {
            return ex is DomainValidationException or StringValidationException
                ? BadRequest(ex.Message)
                : StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UpdateMedicineOutput>> UpdateMedicine(Guid id, [FromBody] UpdateMedicineInput body)
    {
        try
        {
            var result = await updateMedicineUc.ExecuteAsync(id, body);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return ex is DomainValidationException or StringValidationException
                ? BadRequest(ex.Message)
                : StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<DeleteMedicineOutput>> UpdateMedicine(Guid id)
    {
        try
        {
            var result = await deleteMedicineUc.ExecuteAsync(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}