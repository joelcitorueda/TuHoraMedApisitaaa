using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TuHoraMedApisitaaa.Services;
using System.Collections.Generic;
using TuHoraMedApisitaaa.Models;

namespace TuHoraMedApisitaaa.Controllers
{
	[ApiController]
	[Route("api/pacientes/{id}/historial")]
	public class HistorialMedicoController : ControllerBase
	{
		private readonly TuHoraMedService _service;

		public HistorialMedicoController(TuHoraMedService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<HistorialMedico>>> GetHistorial(int id)
		{
			var historial = await _service.ObtenerHistorialPaciente(id);
			return Ok(historial);
		}
	}
}
