using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuHoraMedApisitaaa.Data;
using TuHoraMedApisitaaa.Models;

namespace TuHoraMedApisitaaa.Services
{
	public class TratamientoService
	{
		private readonly TuHoraMedContext _context;

		public TratamientoService(TuHoraMedContext context)
		{
			_context = context;
		}

		public async Task<Tratamiento> ProgramarTratamiento(Tratamiento nuevo)
		{
			// Validaciones simples (puedes expandir con lógica más compleja si deseas)
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
	}
}
