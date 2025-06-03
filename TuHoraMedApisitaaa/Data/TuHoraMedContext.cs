using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TuHoraMedApisitaaa.Models;

namespace TuHoraMedApisitaaa.Data
{
	public class TuHoraMedContext : DbContext
	{
		public TuHoraMedContext(DbContextOptions<TuHoraMedContext> options) : base(options)
		{
		}

		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Tratamiento> Tratamientos { get; set; }
		public DbSet<AdministracionTratamiento> AdministracionesTratamiento { get; set; }
		public DbSet<HistorialMedico> HistorialesMedicos { get; set; }

		public DbSet<Inventario> Inventarios { get; set; }
		public DbSet<PanelPaciente> PanelPacientes { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Usuario>().HasIndex(u => u.Correo).IsUnique();
		}
	}
}
