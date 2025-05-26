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
├── Program.cs                      // App startup and configuration
├── Data/
│   └── ApplicationDBContext.cs    // DbContext for EF Core
├── Models/
│   ├── AppUser.cs                 // Identity user model
│   ├── Stock.cs                   // Stock entity
│   ├── Comment.cs                 // Comment entity
│   └── Portfolio.cs               // Join table for user-stock relation
├── Controllers/
│   ├── StockController.cs         // Handles stock endpoints
│   ├── CommentController.cs       // Handles comment endpoints
│   └── PortfolioController.cs     // Handles user portfolio endpoints
├── DTOs/
│   ├── CreateCommentDto.cs        // DTO for creating a comment
│   ├── StockDto.cs                // DTO for returning stock data
│   └── PortfolioDto.cs            // DTO for portfolio actions
├── Extensions/
│   └── ClaimsExtensions.cs        // Helper to get user info from token
├── Interfaces/
│   └── ITokenService.cs           // Interface for JWT service abstraction
├── Services/
│   └── TokenService.cs            // Responsible for creating JWT tokens
├── Helpers/
│   └── StockMapper.cs             // Maps between models and DTOs (if used)
├── Migrations/                    // EF Core migrations
├── appsettings.json               // Configuration file
├── api.csproj                     // Project file
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
