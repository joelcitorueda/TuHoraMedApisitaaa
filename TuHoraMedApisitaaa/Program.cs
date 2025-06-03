using Microsoft.EntityFrameworkCore;
using TuHoraMedApisitaaa.Data;
using TuHoraMedApisitaaa.Services;

var builder = WebApplication.CreateBuilder(args);

// Conexión a SQL Server
builder.Services.AddDbContext<TuHoraMedContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS - PERMITE ACCESO DESDE EL FRONTEND (puerto 5173 a 5179 por seguridad)
builder.Services.AddCors(options =>
{
	options.AddPolicy("PermitirFrontend", policy =>
	{
		policy.WithOrigins(
			"http://localhost:5173",
			"http://localhost:5174",
			"http://localhost:5175",
			"http://localhost:5176",
			"http://localhost:5177",
			"http://localhost:5178",
			"http://localhost:5180",
			"http://localhost:5179"
		)
		.AllowAnyHeader()
		.AllowAnyMethod();
	});
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<TuHoraMedService>();

var app = builder.Build();

// Habilitar CORS antes de cualquier controlador
app.UseCors("PermitirFrontend");

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
