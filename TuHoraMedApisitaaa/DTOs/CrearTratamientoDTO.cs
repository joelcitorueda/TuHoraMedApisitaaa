using System;
using System.ComponentModel.DataAnnotations;

namespace TuHoraMedApisitaaa.DTOs
{
	public class CrearTratamientoDTO
	{
		[Required]
		public int PacienteId { get; set; }

		[Required]
		public string Medicamento { get; set; }

		[Required]
		public string Dosis { get; set; }

		[Required]
		public string Frecuencia { get; set; }

		[Required]
		public int Duracion { get; set; }

		[Required]
		public DateTime FechaInicio { get; set; }
	}
}
