using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuHoraMedApisitaaa.Data;
using TuHoraMedApisitaaa.Models;

namespace TuHoraMedApisitaaa.Services
{
	public class UsuarioService
	{
		private readonly TuHoraMedContext _context;

		public UsuarioService(TuHoraMedContext context)
		{
			_context = context;
		}

		public async Task<bool> CorreoExiste(string correo)
		{
			return await _context.Usuarios.AnyAsync(u => u.Correo == correo);
		}

		public string Encriptar(string password)
		{
			using var sha256 = SHA256.Create();
			var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
			return Convert.ToBase64String(bytes);
		}

		public async Task<Usuario> RegistrarUsuario(Usuario nuevoUsuario)
		{
			if (await CorreoExiste(nuevoUsuario.Correo))
				return null;

			nuevoUsuario.Contrasena = Encriptar(nuevoUsuario.Contrasena);
			nuevoUsuario.FechaRegistro = DateTime.UtcNow;
			nuevoUsuario.Estado = true;

			_context.Usuarios.Add(nuevoUsuario);
			await _context.SaveChangesAsync();

			return nuevoUsuario;
		}
	}
}
