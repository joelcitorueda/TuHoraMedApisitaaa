using System;
using System.ComponentModel.DataAnnotations;

namespace TuHoraMedApisitaaa.Models
{
	public class Inventario
	{
		[Key]
		public int IdProducto { get; set; }

		[Required]
		public string NombreProducto { get; set; }

		[Required]
		public int StockActual { get; set; }

		[Required]
		public int UmbralMinimo { get; set; }

		public string UnidadMedida { get; set; }

		public string Ubicacion { get; set; }

		public DateTime UltimaActualizacion { get; set; } = DateTime.UtcNow;

		public bool Activo { get; set; } = true;
	}
}
