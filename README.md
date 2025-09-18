# OnionExample (.NET 8, Onion Architecture)

Solución mínima con arquitectura Onion:
- Domain: Entidades y contratos.
- Application: Servicios/orquestación de casos de uso.
- Infrastructure: Implementaciones (repositorios, aquí en memoria).
- API: ASP.NET Core Web API.

## Estructura
```
src/
  OnionExample.Domain/
    Entities/
      Customer.cs
      Order.cs
    Interfaces/
      ICustomerRepository.cs
  OnionExample.Application/
    Services/
      CustomerService.cs
  OnionExample.Infrastructure/
    Repositories/
      CustomerRepository.cs
  OnionExample.API/
    Controllers/
      CustomerController.cs
    Program.cs
```

## Requisitos
- .NET SDK 8.0

## Restaurar, compilar y ejecutar
```powershell
# Windows PowerShell desde la raíz del repo
# 1) (Opcional) Crear la solución y agregar proyectos si aún no existe el .sln
if (-not (Test-Path "OnionExample.sln")) {
  dotnet new sln -n OnionExample
  dotnet sln add src/OnionExample.Domain/OnionExample.Domain.csproj
  dotnet sln add src/OnionExample.Application/OnionExample.Application.csproj
  dotnet sln add src/OnionExample.Infrastructure/OnionExample.Infrastructure.csproj
  dotnet sln add src/OnionExample.API/OnionExample.API.csproj
}

# 2) Restaurar y compilar
dotnet restore
dotnet build -c Release

# 3) Ejecutar API (Swagger habilitado en Development)
dotnet run --project src/OnionExample.API/OnionExample.API.csproj
```

Swagger en desarrollo: `https://localhost:5001/swagger` (el puerto puede variar según Kestrel/HTTPS dev cert).

## Endpoints básicos
- GET `api/customer`
- GET `api/customer/{id}`
- POST `api/customer`
- PUT `api/customer/{id}`
- DELETE `api/customer/{id}`

Ejemplo POST (Swagger o curl):
```json
{
  "firstName": "Ada",
  "lastName": "Lovelace",
  "email": "ada@example.com"
}
```

## Notas
- Repositorio en memoria (los datos se pierden al reiniciar la app).
- `Order` es el segundo modelo básico incluido para ilustrar el dominio; esta versión mínima no expone endpoints de órdenes.
