using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TuHoraMedApisitaaa.Services;

namespace TuHoraMedApisitaaa.Controllers
{
	[ApiController]
	[Route("api/tratamientos")]
	public class ControlTratamientosController : ControllerBase
	{
		private readonly TuHoraMedService _service;

		public ControlTratamientosController(TuHoraMedService service)
		{
			_service = service;
		}

		[HttpPost("{id}/confirmar")]
		public async Task<IActionResult> Confirmar(int id, [FromBody] ConfirmacionDto dto)
		{
			var success = await _service.ConfirmarTratamiento(id, dto.IdCuidador, dto.DosisAdministrada, dto.Observaciones);
			if (!success) return BadRequest("No se pudo confirmar el tratamiento.");
			return Ok("Tratamiento confirmado.");
		}

		[HttpPost("{id}/omitir")]
		public async Task<IActionResult> Omitir(int id, [FromBody] OmisionDto dto)
		{
			if (string.IsNullOrWhiteSpace(dto.MotivoOmision))
				return BadRequest("Motivo de omisión obligatorio.");

			var success = await _service.OmitirTratamiento(id, dto.IdCuidador, dto.MotivoOmision, dto.Observaciones);
			if (!success) return BadRequest("No se pudo omitir el tratamiento.");
			return Ok("Tratamiento omitido.");
		}

		public class ConfirmacionDto
		{
			public int IdCuidador { get; set; }
			public string DosisAdministrada { get; set; }
			public string Observaciones { get; set; }
		}

		public class OmisionDto
		{
			public int IdCuidador { get; set; }
			public string MotivoOmision { get; set; }
			public string Observaciones { get; set; }
		}
	}
}
