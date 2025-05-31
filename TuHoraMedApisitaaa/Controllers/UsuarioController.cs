using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TuHoraMedApisitaaa.Models;
using TuHoraMedApisitaaa.Services;
using System.Collections.Generic;

namespace TuHoraMedApisitaaa.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsuarioController : ControllerBase
	{
		private readonly TuHoraMedService _service;

		public UsuarioController(TuHoraMedService service)
		{
			_service = service;
		}

		[HttpPost]
		public async Task<IActionResult> RegistrarUsuario([FromBody] Usuario usuario)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var registrado = await _service.RegistrarUsuario(usuario);

			if (registrado == null)
				return Conflict("El correo ya está registrado.");

			return Ok(registrado);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Usuario>>> ListarUsuarios()
		{
			var usuarios = await _service.ObtenerUsuarios();
			return Ok(usuarios);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> EditarUsuario(int id, [FromBody] Usuario actualizado)
		{
			var resultado = await _service.ActualizarUsuario(id, actualizado);
			if (!resultado)
				return NotFound("Usuario no encontrado.");
			return Ok("Usuario actualizado correctamente.");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> EliminarUsuario(int id)
		{
			var resultado = await _service.EliminarUsuario(id);
			if (!resultado)
				return NotFound("Usuario no encontrado.");
			return Ok("Usuario eliminado correctamente.");
		}
	}
}
