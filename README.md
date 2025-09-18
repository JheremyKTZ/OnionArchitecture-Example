OnionExample (.NET 8, Onion Architecture)
Minimal solution with Onion Architecture:
- Domain: Entities and contracts.
- Application: Services / use case orchestration.
- Infrastructure: Implementations (repositories, here in-memory).
- API: ASP.NET Core Web API.

## Structure
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

## Requirements
- .NET SDK 8.0

## Restore, Build, and Run
```powershell
# Windows PowerShell from the root of the repo
# 1) (Optional) Create the solution and add projects if the .sln does not exist
if (-not (Test-Path "OnionExample.sln")) {
  dotnet new sln -n OnionExample
  dotnet sln add src/OnionExample.Domain/OnionExample.Domain.csproj
  dotnet sln add src/OnionExample.Application/OnionExample.Application.csproj
  dotnet sln add src/OnionExample.Infrastructure/OnionExample.Infrastructure.csproj
  dotnet sln add src/OnionExample.API/OnionExample.API.csproj
}

# 2) Restore and build
dotnet restore
dotnet build -c Release

# 3) Run the API (Swagger enabled in Development)
dotnet run --project src/OnionExample.API/OnionExample.API.csproj
```

Swagger in development: https://localhost:5001/swagger (port may vary depending on Kestrel / HTTPS dev cert).

## Basic Endpoints
- GET `api/customer`
- GET `api/customer/{id}`
- POST `api/customer`
- PUT `api/customer/{id}`
- DELETE `api/customer/{id}`

Example POST (via Swagger or curl):```json
{
  "firstName": "Ada",
  "lastName": "Lovelace",
  "email": "ada@example.com"
}
```

Notes
- In-memory repository (data is lost when the app restarts).
- Order is included as a second basic model to illustrate the domain; this minimal version does not expose order endpoints.
