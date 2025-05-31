using System;
using System.ComponentModel.DataAnnotations;

namespace TuHoraMedApisitaaa.Models
{
	public class Usuario
	{
		[Key]
		public int IdUsuario { get; set; }

		[Required]
		public string Nombre { get; set; }

		[Required]
		public string Apellido { get; set; }

		[Required]
		[EmailAddress]
		public string Correo { get; set; }

		public string Telefono { get; set; }

		[Required]
		public string Rol { get; set; }

		[Required]
		public string Contrasena { get; set; }

		public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

		public bool Estado { get; set; } = true;
	}
}
