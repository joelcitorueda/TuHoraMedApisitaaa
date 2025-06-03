using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuHoraMedApisitaaa.Models
{
	public class AdministracionTratamiento
	{
		[Key]
		public int IdAdministracion { get; set; }

		[Required]
		public int IdTratamiento { get; set; }

		[ForeignKey("IdTratamiento")]
		public Tratamiento Tratamiento { get; set; }

		[Required]
		public int IdCuidador { get; set; }

		public DateTime FechaHoraRegistro { get; set; } = DateTime.UtcNow;

		[Required]
		public string TipoAccion { get; set; } // "Confirmado" o "Omitido"

		public string MotivoOmision { get; set; }

		public string DosisAdministrada { get; set; }

		public string Observaciones { get; set; }

		public bool EstadoValidacion { get; set; } = false;
	}
}
