using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuHoraMedApisitaaa.Models
{
	public class Tratamiento
	{
		[Key]
		public int IdTratamiento { get; set; }

		[Required]
		public int PacienteId { get; set; }

		[ForeignKey("PacienteId")]
		public Usuario Paciente { get; set; }

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
