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

		public async Task<Usuario> ObtenerUsuarioPorId(int id)
		{
			return await _context.Usuarios.FindAsync(id);
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

		public async Task<List<Tratamiento>> ObtenerTodosTratamientos()
		{
			return await _context.Tratamientos
				.Include(t => t.Paciente)  // Carga el paciente relacionado
				.OrderByDescending(t => t.FechaInicio)
				.ToListAsync();
		}

		public async Task<List<Tratamiento>> ObtenerTratamientos()
		{
			return await _context.Tratamientos
				.Include(t => t.Paciente) // esto carga la entidad paciente relacionada
				.OrderByDescending(t => t.FechaInicio)
				.ToListAsync();
		}

		public async Task<List<Tratamiento>> ObtenerPorPaciente(int pacienteId)
		{
			return await _context.Tratamientos
				.Where(t => t.PacienteId == pacienteId)
				.Include(t => t.Paciente)   // Aquí incluye la entidad relacionada
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

		//Administracion de tratamiento 
		public async Task<bool> ConfirmarTratamiento(int idTratamiento, int idCuidador, string dosisAdministrada, string observaciones = null)
		{
			var tratamiento = await _context.Tratamientos.FindAsync(idTratamiento);
			if (tratamiento == null) return false;

			var registro = new AdministracionTratamiento
			{
				IdTratamiento = idTratamiento,
				IdCuidador = idCuidador,
				TipoAccion = "Confirmado",
				DosisAdministrada = dosisAdministrada,
				Observaciones = observaciones,
				FechaHoraRegistro = DateTime.UtcNow,
				EstadoValidacion = false
			};

			_context.AdministracionesTratamiento.Add(registro);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> OmitirTratamiento(int idTratamiento, int idCuidador, string motivoOmision, string observaciones = null)
		{
			if (string.IsNullOrWhiteSpace(motivoOmision))
				return false; // Validación: motivo obligatorio para omitir

			var tratamiento = await _context.Tratamientos.FindAsync(idTratamiento);
			if (tratamiento == null) return false;

			var registro = new AdministracionTratamiento
			{
				IdTratamiento = idTratamiento,
				IdCuidador = idCuidador,
				TipoAccion = "Omitido",
				MotivoOmision = motivoOmision,
				Observaciones = observaciones,
				FechaHoraRegistro = DateTime.UtcNow,
				EstadoValidacion = false
			};

			_context.AdministracionesTratamiento.Add(registro);
			await _context.SaveChangesAsync();
			return true;
		}

		//Inventariooooooooooooo

		// Obtener inventario completo
		public async Task<List<Inventario>> ObtenerInventario()
		{
			return await _context.Inventarios
				.Where(i => i.Activo)
				.OrderBy(i => i.NombreProducto)
				.ToListAsync();
		}

		// Actualizar stock de producto
		public async Task<bool> ActualizarStock(int idProducto, int cantidadCambio)
		{
			var producto = await _context.Inventarios.FindAsync(idProducto);
			if (producto == null) return false;

			producto.StockActual += cantidadCambio;
			producto.UltimaActualizacion = DateTime.UtcNow;

			_context.Inventarios.Update(producto);
			await _context.SaveChangesAsync();
			return true;
		}

		// Configurar umbral mínimo
		public async Task<bool> ConfigurarUmbral(int idProducto, int nuevoUmbral)
		{
			var producto = await _context.Inventarios.FindAsync(idProducto);
			if (producto == null) return false;

			producto.UmbralMinimo = nuevoUmbral;
			producto.UltimaActualizacion = DateTime.UtcNow;

			_context.Inventarios.Update(producto);
			await _context.SaveChangesAsync();
			return true;
		}

		// Obtener alertas de bajo stock
		public async Task<List<Inventario>> ObtenerAlertasBajoStock()
		{
			return await _context.Inventarios
				.Where(i => i.StockActual < i.UmbralMinimo && i.Activo)
				.ToListAsync();
		}



		//Historial de paciente

		public async Task<List<HistorialMedico>> ObtenerHistorialPaciente(int idPaciente)
		{
			return await _context.HistorialesMedicos
				.Where(h => h.IdPaciente == idPaciente)
				.OrderByDescending(h => h.FechaHoraEvento)
				.ToListAsync();
		}

		// Obtener todos los pacientes en el panel de seguimiento general
		public async Task<List<PanelPaciente>> ObtenerPanelPacientes()
		{
			return await _context.PanelPacientes
				.OrderBy(p => p.NombrePaciente)
				.ToListAsync();
		}

		// Actualizar el estado de un paciente en el panel
		public async Task<bool> ActualizarEstadoPaciente(int idPanel, string nuevoEstado, string prioridad, bool notificaciones)
		{
			var paciente = await _context.PanelPacientes.FindAsync(idPanel);
			if (paciente == null) return false;

			paciente.Estado = nuevoEstado;
			paciente.Prioridad = prioridad;
			paciente.Notificaciones = notificaciones;
			paciente.UltimaActualizacion = System.DateTime.UtcNow;

			_context.PanelPacientes.Update(paciente);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
