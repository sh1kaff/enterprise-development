using Microsoft.AspNetCore.Mvc;
using TransportCompany.Contracts.Dto;
using TransportCompany.Contracts.Interfaces;

namespace TransportCompany.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriversController : ControllerBase
{
    private readonly IDriverService _service;
    private readonly ILogger<DriversController> _logger;

    public DriversController(IDriverService service, ILogger<DriversController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DriverDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<DriverDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetAllAsync(cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка водителей");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(DriverDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<DriverDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null) return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении водителя {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(DriverDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<DriverDto>> Create([FromBody] DriverEditDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании водителя");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Update(int id, [FromBody] DriverEditDto dto, CancellationToken cancellationToken)
    {
        try
        {
            await _service.UpdateAsync(id, dto, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении водителя {Id}", id);
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
            _logger.LogError(ex, "Ошибка при удалении водителя {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}