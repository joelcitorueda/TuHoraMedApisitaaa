using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuHoraMedApisitaaa.Data;
using TuHoraMedApisitaaa.Models;
using System.Security.Cryptography;
using System.Text;
using System;

namespace TuHoraMedApisitaaa.Services
{
	public class TuHoraMedService
	{
		private readonly TuHoraMedContext _context;

		public TuHoraMedService(TuHoraMedContext context)
		{
			_context = context;
		}

		// 🔹 Usuario

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

		public async Task<Usuario> RegistrarUsuario(Usuario nuevo)
		{
			if (await CorreoExiste(nuevo.Correo))
				return null;

			nuevo.Contrasena = Encriptar(nuevo.Contrasena);
			nuevo.FechaRegistro = DateTime.UtcNow;
			nuevo.Estado = true;

			_context.Usuarios.Add(nuevo);
			await _context.SaveChangesAsync();
			return nuevo;
		}
		public async Task<List<Usuario>> ObtenerUsuarios()
		{
			return await _context.Usuarios
				.OrderBy(u => u.Nombre)
				.ToListAsync();
		}

		public async Task<bool> ActualizarUsuario(int id, Usuario actualizado)
		{
			var usuario = await _context.Usuarios.FindAsync(id);
			if (usuario == null) return false;

			usuario.Nombre = actualizado.Nombre;
			usuario.Apellido = actualizado.Apellido;
			usuario.Telefono = actualizado.Telefono;
			usuario.Rol = actualizado.Rol;
			usuario.Estado = actualizado.Estado;

			_context.Usuarios.Update(usuario);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> EliminarUsuario(int id)
		{
			var usuario = await _context.Usuarios.FindAsync(id);
			if (usuario == null) return false;

			_context.Usuarios.Remove(usuario);
			await _context.SaveChangesAsync();
			return true;
		}


		// 🔹 Tratamiento

		public async Task<Tratamiento> ProgramarTratamiento(Tratamiento nuevo)
		{
			if (string.IsNullOrEmpty(nuevo.Medicamento) || string.IsNullOrEmpty(nuevo.Dosis))
				return null;

			_context.Tratamientos.Add(nuevo);
			await _context.SaveChangesAsync();
			return nuevo;
		}

		public async Task<List<Tratamiento>> ObtenerPorPaciente(int pacienteId)
		{
			return await _context.Tratamientos
				.Where(t => t.PacienteId == pacienteId)
				.OrderByDescending(t => t.FechaInicio)
				.ToListAsync();
		}
		public async Task<bool> ActualizarTratamiento(int id, Tratamiento actualizado)
		{
			var tratamiento = await _context.Tratamientos.FindAsync(id);
			if (tratamiento == null) return false;

			tratamiento.Medicamento = actualizado.Medicamento;
			tratamiento.Dosis = actualizado.Dosis;
			tratamiento.Frecuencia = actualizado.Frecuencia;
			tratamiento.Duracion = actualizado.Duracion;
			tratamiento.FechaInicio = actualizado.FechaInicio;

			_context.Tratamientos.Update(tratamiento);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> EliminarTratamiento(int id)
		{
			var tratamiento = await _context.Tratamientos.FindAsync(id);
			if (tratamiento == null) return false;

			_context.Tratamientos.Remove(tratamiento);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
