## CREAR BDD

- Abrir Consola del Administrador de paquetes [Herramientas/Administrador de Paquetes Nuget/Consola del Administrador de Paquetes]

```
    Add-Migration Inicial
```

- Se crea el archivo que se va a ejecutar cuando se realice la migración
- Actualizar la Base de Datos:

```
    Update-Database
```

```
npm install react@latest react-dom@latest

Scaffold-DbContext "Server=ELESSAR; DataBase=AssessmentNet;Integrated Security=false; TrustServerCertificate=True; User Id=sa;Password=P@$$w0rd;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models

dotnet ef dbcontext scaffold "Server=ELESSAR; DataBase=AssessmentNet;Integrated Security=false; TrustServerCertificate=True; User Id=sa;Password=P@$$w0rd;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models
```
