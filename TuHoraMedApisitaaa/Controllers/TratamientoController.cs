using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TuHoraMedApisitaaa.Models;
using TuHoraMedApisitaaa.Services;
using System.Collections.Generic;
using TuHoraMedApisitaaa.DTOs;

namespace TuHoraMedApisitaaa.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TratamientoController : ControllerBase
	{
		private readonly TuHoraMedService _service;

		public TratamientoController(TuHoraMedService service)
		{
			_service = service;
		}

		[HttpPost("programar")]
		public async Task<IActionResult> Programar([FromBody] CrearTratamientoDTO tratamientoDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var nuevoTratamiento = new Tratamiento
			{
				PacienteId = tratamientoDto.PacienteId,
				Medicamento = tratamientoDto.Medicamento,
				Dosis = tratamientoDto.Dosis,
				Frecuencia = tratamientoDto.Frecuencia,
				Duracion = tratamientoDto.Duracion,
				FechaInicio = tratamientoDto.FechaInicio
			};

			var nuevo = await _service.ProgramarTratamiento(nuevoTratamiento);
			if (nuevo == null)
				return BadRequest("Datos inválidos.");
			return Ok(nuevo);
		}

		[HttpGet("{idPaciente}")]
		public async Task<ActionResult<IEnumerable<Tratamiento>>> ObtenerPorPaciente(int idPaciente)
		{
			var tratamientos = await _service.ObtenerPorPaciente(idPaciente);
			return Ok(tratamientos);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> EditarTratamiento(int id, [FromBody] Tratamiento actualizado)
		{
			var resultado = await _service.ActualizarTratamiento(id, actualizado);
			if (!resultado)
				return NotFound("Tratamiento no encontrado.");
			return Ok("Tratamiento actualizado correctamente.");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> EliminarTratamiento(int id)
		{
			var resultado = await _service.EliminarTratamiento(id);
			if (!resultado)
				return NotFound("Tratamiento no encontrado.");
			return Ok("Tratamiento eliminado correctamente.");
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Tratamiento>>> ObtenerTodos()
		{
			var tratamientos = await _service.ObtenerTratamientos();
			return Ok(tratamientos);
		}


	}
}
