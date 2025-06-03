using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TuHoraMedApisitaaa.Services;
using System.Collections.Generic;
using TuHoraMedApisitaaa.Models;

namespace TuHoraMedApisitaaa.Controllers
{
	[ApiController]
	[Route("api/panel")]
	public class PanelController : ControllerBase
	{
		private readonly TuHoraMedService _service;

		public PanelController(TuHoraMedService service)
		{
			_service = service;
		}

		// GET: api/panel/pacientes
		[HttpGet("pacientes")]
		public async Task<ActionResult<IEnumerable<PanelPaciente>>> GetPacientes()
		{
			var pacientes = await _service.ObtenerPanelPacientes();
			return Ok(pacientes);
		}

		// PUT: api/panel/pacientes/{id}
		[HttpPut("pacientes/{id}")]
		public async Task<IActionResult> ActualizarEstado(int id, [FromBody] PanelPaciente actualizado)
		{
			var result = await _service.ActualizarEstadoPaciente(id, actualizado.Estado, actualizado.Prioridad, actualizado.Notificaciones);
			if (!result)
				return NotFound("Paciente no encontrado.");
			return Ok("Estado actualizado correctamente.");
		}
	}
}
