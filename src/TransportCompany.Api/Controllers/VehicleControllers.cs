using Microsoft.AspNetCore.Mvc;
using TransportCompany.Contracts.Dto;
using TransportCompany.Contracts.Interfaces;

namespace TransportCompany.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _service;
    private readonly ILogger<VehiclesController> _logger;

    public VehiclesController(IVehicleService service, ILogger<VehiclesController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VehicleDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<VehicleDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetAllAsync(cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка ТС");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(VehicleDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<VehicleDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null) return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении ТС {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(VehicleDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<VehicleDto>> Create([FromBody] VehicleEditDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании ТС");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Update(int id, [FromBody] VehicleEditDto dto, CancellationToken cancellationToken)
    {
        try
        {
            await _service.UpdateAsync(id, dto, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении ТС {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении ТС {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}