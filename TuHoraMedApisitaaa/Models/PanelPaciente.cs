using System;
using System.ComponentModel.DataAnnotations;

namespace TuHoraMedApisitaaa.Models
{
	public class PanelPaciente
	{
		[Key]
		public int IdPanel { get; set; }

		[Required]
		public int IdPaciente { get; set; }

		public string NombrePaciente { get; set; }

		public string Estado { get; set; }

		public DateTime UltimaActualizacion { get; set; }

		public string Especialidad { get; set; }

		public string Prioridad { get; set; }

		public bool Notificaciones { get; set; }

		public int LatenciaSegundos { get; set; }
	}
}
