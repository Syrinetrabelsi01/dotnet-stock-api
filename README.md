# DotNet Stock API

This is a learning project I followed from YouTube, based on the [FinShark API by Teddy Smith](https://github.com/teddysmithdev/FinShark). I used it to understand how to build a RESTful API with ASP.NET Core, Entity Framework, and MySQL.

---

## Features

- CRUD operations for stocks, comments, and portfolios
- JWT authentication for login and secure endpoints
- Identity-based user management
- Uses DTOs for clean data handling
- Swagger integration for testing the API

---

## Project Structure

```
├── Program.cs                        // App entry point and configuration
├── api.csproj                        // Project file with dependencies
├── appsettings.json                  // Main config (connection strings, JWT keys)
├── appsettings.Development.json      // Development-specific settings
├── Data/
│   └── ApplicationDBContext.cs       // EF Core DbContext
├── Models/
│   ├── AppUser.cs                    // Identity user entity
│   ├── Comment.cs                    // Comment entity
│   ├── Portfolio.cs                  // Join entity: user <-> stock
│   └── Stock.cs                      // Stock entity
├── Controllers/
│   ├── AccountController.cs          // Login & register endpoints
│   ├── CommentController.cs          // Comment endpoints
│   ├── PortfolioController.cs        // Portfolio endpoints
│   └── StockController.cs            // Stock endpoints
├── Dtos/
│   ├── Account/                      // DTOs for account operations
│   │   ├── LoginDto.cs
│   │   ├── RegisterDto.cs
│   │   └── NewUserDto.cs
│   ├── Comment/                      // DTOs for comments
│   │   ├── CommentDto.cs
│   │   ├── CreateCommentDto.cs
│   │   └── UpdateCommentRequestDto.cs
│   └── Stock/                        // DTOs for stock
│       ├── StockCreateDto.cs
│       ├── StockDto.cs
│       └── StockUpdateDto.cs
├── Interfaces/
│   ├── ICommentRepository.cs         // Comment repo interface
│   ├── IFMPService.cs                // External stock API interface
│   ├── IPortfolioRepository.cs       // Portfolio repo interface
│   ├── IStockRepository.cs           // Stock repo interface
│   └── ITokenService.cs              // JWT service interface
├── Repository/
│   ├── CommentRepository.cs          // Handles DB logic for comments
│   ├── PortfolioRepository.cs        // Handles DB logic for portfolios
│   └── StockRepository.cs            // Handles DB logic for stocks
├── Service/
│   ├── FMPService.cs                 // External stock market API service
│   └── TokenService.cs              // JWT token generation logic
├── Mappers/
│   ├── CommentMapper.cs              // Maps Comment ↔ CommentDto
│   └── StockMappers.cs               // Maps Stock ↔ StockDto
├── Helper/
│   ├── CommentQueryObject.cs         // Filtering logic for comments
│   ├── QueryObject.cs                // Generic query filters
│   └── StockExtensions.cs            // Helper methods for stock logic
├── Extensions/
│   └── ClaimsExtensions.cs           // Extracts claims from JWT
├── Migrations/                       // EF Core DB migrations
│   ├── [timestamp]_*.cs              // Auto-generated migration scripts
│   └── ApplicationDBContextModelSnapshot.cs
├── Properties/
│   └── launchSettings.json           // Launch profiles for development
```


---

## How it works

1. The user sends a request from a client or Swagger.
2. The controller handles the request and uses the DTO to validate the input.
3. Entity Framework interacts with the database using ApplicationDBContext.
4. A response is returned to the client.

---

## Authentication

- Users register and login.
- On login, they receive a JWT token.
- Token must be included in the Authorization header for protected routes.

---

## Swagger

- Available at `https://localhost:<port>/swagger`
- Only enabled in development mode
- Useful for testing endpoints during development

---

## Tech Stack

- ASP.NET Core
- Entity Framework Core
- MySQL
- Identity + JWT for authentication
- Swagger for API testing

---

## Getting Started

### Prerequisites
- .NET SDK (version 7.0 or later)
- MySQL installed and running
- Entity Framework Core CLI tools (`dotnet-ef`)

### Setup Instructions

```bash
# 1. Restore project dependencies
$ dotnet restore

# 2. Update the database schema (creates tables if needed)
$ dotnet ef database update

# 3. Run the API
$ dotnet run
```

Once running, open your browser and go to:
```
https://localhost:<your-port>/swagger
```
to test the API using Swagger UI.

---

## Notes

This is a simplified version of the FinShark API. I used it to learn how to structure a backend project, work with EF Core, and implement JWT authentication. Most of the logic is based on Teddy Smith’s original repository.
