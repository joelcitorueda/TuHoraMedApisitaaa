using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TuHoraMedApisitaaa.Services;
using System.Collections.Generic;
using TuHoraMedApisitaaa.Models;

namespace TuHoraMedApisitaaa.Controllers
{
	[ApiController]
	[Route("api/inventario")]
	public class InventarioController : ControllerBase
	{
		private readonly TuHoraMedService _service;

		public InventarioController(TuHoraMedService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Inventario>>> GetInventario()
		{
			var inventario = await _service.ObtenerInventario();
			return Ok(inventario);
		}

		[HttpPost("{id}/actualizar_stock")]
		public async Task<IActionResult> ActualizarStock(int id, [FromBody] int cantidad)
		{
			var result = await _service.ActualizarStock(id, cantidad);
			if (!result) return NotFound("Producto no encontrado.");
			return Ok("Stock actualizado correctamente.");
		}

		[HttpPut("{id}/configurar_umbral")]
		public async Task<IActionResult> ConfigurarUmbral(int id, [FromBody] int nuevoUmbral)
		{
			var result = await _service.ConfigurarUmbral(id, nuevoUmbral);
			if (!result) return NotFound("Producto no encontrado.");
			return Ok("Umbral configurado correctamente.");
		}

		[HttpGet("alertas_bajo_stock")]
		public async Task<ActionResult<IEnumerable<Inventario>>> GetAlertasBajoStock()
		{
			var alertas = await _service.ObtenerAlertasBajoStock();
			return Ok(alertas);
		}
	}
}
