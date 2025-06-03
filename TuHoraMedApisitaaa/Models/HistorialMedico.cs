using System;
using System.ComponentModel.DataAnnotations;

namespace TuHoraMedApisitaaa.Models
{
	public class HistorialMedico
	{
		[Key]
		public int IdRegistroHistorial { get; set; }

		[Required]
		public int IdPaciente { get; set; }

		[Required]
		public string TipoRegistro { get; set; } // Ej: "Toma de Medicamento", "Alerta", etc.

		[Required]
		public DateTime FechaHoraEvento { get; set; }

		public string DescripcionEvento { get; set; }

		public int? IdReferencia { get; set; }

		public string RegistradoPor { get; set; }

		public string NotasAdicionales { get; set; }
	}
}
