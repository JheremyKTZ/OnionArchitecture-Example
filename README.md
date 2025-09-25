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
```


## Swagger API Overview

![Swagger API Overview](./docs/swagger_api_overview.png)



## Hands-on Lab: Learn Onion Architecture by Doing

Follow these short exercises to explore the layers and see why interfaces matter.

### 0) Prerequisites
- **.NET SDK 8.0** installed
- PowerShell (Windows) or your preferred shell

### 1) Run the API (Development mode)
```powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet restore
dotnet build -c Release
dotnet run --project src/OnionExample.API/OnionExample.API.csproj
```
Open Swagger: `http://localhost:5000/swagger`

### 2) Quick tour of layers (read-only)
- **Domain**: entities and contracts (e.g., `Entities/Customer.cs`, `Interfaces/ICustomerRepository.cs`)
- **Application**: use cases/services (e.g., `Services/CustomerService.cs` implements `ICustomerService`)
- **Infrastructure**: implementations (e.g., `Repositories/CustomerRepository.cs`)
- **API**: controllers/DI startup (e.g., `Controllers/CustomerController.cs`, `Program.cs`)

### 3) CRUD Customers via Swagger
Use the `Customer` endpoints to see the flow Controller → Service → Repository.
- **POST** `api/customer` with body:
```json
{
  "firstName": "Ada",
  "lastName": "Lovelace",
  "email": "ada@example.com"
}
```
- **GET** `api/customer` (list)
- **GET** `api/customer/{id}` (detail)
- **PUT** `api/customer/{id}` (update)
- **DELETE** `api/customer/{id}` (remove)

### 4) Exercise: Add Order contracts and service (you code this)
Goal: replicate the Customer flow for `Order` to understand layering.
1. In `OnionExample.Domain/Interfaces`, create `IOrderRepository` with methods similar to `ICustomerRepository`.
2. In `OnionExample.Infrastructure/Repositories`, create `OrderRepository : IOrderRepository` (in-memory like `CustomerRepository`).
3. In `OnionExample.Application/Services`, create `IOrderService` and `OrderService : IOrderService` that delegate to the repository.
4. In `OnionExample.API`, add `OrderController` with endpoints similar to `CustomerController`.
5. Update DI in `Program.cs`:
```csharp
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
```
6. Re-build and test the new endpoints in Swagger.

Hints:
- Propagate `CancellationToken` from controller down to service/repository.
- Mirror how `ICustomerService` is registered in DI.

### 5) Exercise: Swap implementations via DI
Goal: understand how interfaces decouple layers.
1. Create `FakeCustomerRepository : ICustomerRepository` that returns fixed seed data.
2. In `Program.cs`, replace the registration:
```csharp
// builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<ICustomerRepository, FakeCustomerRepository>();
```
3. Run the API and observe how data changes without touching controller/service.

### 6) Optional: Quick test of Application layer
1. Create a test project (xUnit):
```powershell
dotnet new xunit -n OnionExample.Tests
dotnet sln add OnionExample.Tests/OnionExample.Tests.csproj
dotnet add OnionExample.Tests/OnionExample.Tests.csproj reference src/OnionExample.Application/OnionExample.Application.csproj src/OnionExample.Domain/OnionExample.Domain.csproj
dotnet add OnionExample.Tests/OnionExample.Tests.csproj package Moq
```
2. Write a test for `CustomerService` by mocking `ICustomerRepository` with Moq and assert it delegates correctly.
3. Run: `dotnet test`

### 7) Cleanup / Next steps
- Restore `CustomerRepository` if you swapped the implementation.
- Add validations in Application (e.g., required email) and tests.
- Explore real persistence (EF Core) by creating an `EfCustomerRepository` without changing controllers or services.
