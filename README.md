#TuHoraMedApisitaaa - Backend ASP.NET Core

API RESTful para administrar tratamientos y usuarios en centros geriátricos.

##Requisitos previos

- [.NET 7.0 o 6.0 SDK](https://dotnet.microsoft.com/)
- SQL Server Express o completo
- Visual Studio 2022 o VS Code
- Git

---

##  Instrucciones para ejecutar y migrar el proyecto

###  1. Clonar el proyecto

```bash
git clone https://github.com/joelcitorueda/TuHoraMedApisitaaa.git
cd TuHoraMedApisitaaa


###  2. Confiigurar la conexion a SQL Server
Abre el archivo appsettings.json y edita la cadena de conexión con el nombre de tu servidor SQL:

json
Copiar código
"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-NOMBRE;Database=TuHoraMedDB;Trusted_Connection=True;TrustServerCertificate=True"
}
Reemplaza DESKTOP-NOMBRE por el nombre real de tu servidor, que puedes ver en SQL Server Management Studio (SSMS).

###  3. Restaurar paquetes Nuget

Desde la terminal o la consola de Visual Studio, ejecuta:

comando: dotnet restore

###  4. Migrar la base de datos
Abre Visual Studio y entra en:
Herramientas > Administrador de paquetes NuGet > Consola del Administrador de paquetes
Luego ejecuta:
Codigo: Update-Database
Si es la primera vez y no tienes migraciones, usa:

Add-Migration InitialCreate
Update-Database
Esto creará la base de datos TuHoraMedDB con las tablas Usuarios y Tratamientos.