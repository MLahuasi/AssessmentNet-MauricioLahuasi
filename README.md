## CREAR BDD

- Configurar la cadena de [conexion](./Queue.Web/appsettings.json) en:

```
  "ConnectionStrings": {
    "DefaultConnection": "Server=SERVER; DataBase=Database;Integrated Security=false; TrustServerCertificate=True; User Id=sa;Password=123456;"
  }
```

- Crear la migracion: Abrir Consola del Administrador de paquetes [Herramientas/Administrador de Paquetes Nuget/Consola del Administrador de Paquetes] y Ejecutar

```
    Add-Migration Inicial
```

> - **RESULTADO**: Se crea un archivo con la [migración](./Queue.Web/Migrations/20231108004119_Inicial.cs)

- Actualizar migración en la Base de Datos: Abrir Consola del Administrador de paquetes [Herramientas/Administrador de Paquetes Nuget/Consola del Administrador de Paquetes] y Ejecutar.

```
    Update-Database
```

## APLICACION WEB

- Requisitos

  > - Node Js [Version usada v18.17.1]
  > - La App es .NET Core con React Js
  > - Ingresar al directorio [ClientApp](./Queue.Web/ClientApp/) mediante consola y ejecutar:

  ```
        npm install
  ```
