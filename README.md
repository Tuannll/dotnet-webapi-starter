<div align="center">

# 🚀 ASP.NET Core Web API Starter

**Clean, RESTful, purely asynchronous, and thread-safe Web API template built with .NET 10 & C# 13.**

[![CI](https://img.shields.io/github/actions/workflow/status/Tuannll/dotnet-webapi-starter/ci.yml?branch=main&logo=githubactions&logoColor=white)](https://github.com/Tuannll/dotnet-webapi-starter/actions/workflows/ci.yml)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-13.0-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![OpenAPI](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?logo=swagger&logoColor=black)](https://swagger.io/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

**English** | [Tiếng Việt](README.vi.md)

**[✨ Features](#features) • [🚀 Quick Start](#quick-start) • [📁 Structure](#structure) • [📡 Endpoints](#endpoints) • [❓ FAQ](#faq)**

</div>

---

<a id="features"></a>
## ✨ Features

| Domain | Description |
| ---- | -------------- |
| **3-Tier Architecture** | Clean separation of concerns: `Controllers` ➔ `Services` ➔ `Infrastructures`. |
| **Zero Setup DB** | Built-in in-memory mock store—run immediately without installing SQL Server or Docker. |
| **Thread-Safe Storage** | Safe concurrency via `ConcurrentDictionary` and atomic primary key assignment using `Interlocked.Increment`. |
| **100% Async I/O** | All controller and service operations are non-blocking via `Task<T>`. |
| **Centralized Error Handling** | `GlobalExceptionMiddleware` captures exceptions and standardizes responses: `{ isSuccess, statusCode, errorCode, message }`. |
| **Model Validation** | Automatic bad-request rejection via DataAnnotations and customized `InvalidModelStateResponseFactory`. |
| **Ready-to-Test Tooling** | Interactive Swagger UI alongside a pre-configured `WebAPIDemo.http` file containing 12 ready-to-run requests. |

---

<a id="quick-start"></a>
## 🚀 Quick Start

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or higher.

### 1. Clone & Run

```bash
git clone https://github.com/Tuannll/dotnet-webapi-starter.git
cd dotnet-webapi-starter
dotnet run
```

### 2. Interactive API Documentation (Swagger UI)

Open in your browser:
- **Swagger UI:** `http://localhost:5065/swagger`
- **OpenAPI Schema:** `http://localhost:5065/openapi/v1.json`

> [!TIP]
> Open `WebAPIDemo.http` directly in VS Code (with the REST Client extension) or Visual Studio to trigger automated test requests without Postman.

---

<a id="structure"></a>
## 📁 Project Structure

```text
dotnet-webapi-starter/
├── Controllers/              # HTTP Request handling, routing, and HTTP status codes
│   ├── ProductsController.cs
│   └── StudentsController.cs
├── Dtos/                     # Data Transfer Objects & Validation Rules
│   ├── FilterDto.cs          # Base pagination and search filters
│   ├── PagingDto.cs          # Generic pagination response envelope
│   ├── Products/
│   └── Students/
├── Exceptions/               # Custom business domain exceptions
│   ├── ErrorCodes.cs
│   └── UserFriendlyException.cs
├── Infrastructures/          # Persistence layer (Thread-safe In-Memory Store)
│   ├── ApplicationDbContext.cs
│   └── IApplicationDbContext.cs
├── Middleware/               # Global exception interception & standardized response envelopes
│   └── GlobalExceptionMiddleware.cs
├── Models/                   # Domain Entities
│   ├── Product.cs
│   └── Student.cs
├── Services/                 # Business logic implementation
│   ├── Abstracts/
│   └── Implements/
├── Program.cs                # Dependency injection, middleware pipeline, and Swagger configuration
├── WebAPIDemo.http           # 12 automated API test scenarios
└── WebAPIDemo.csproj
```

---

<a id="endpoints"></a>
## 📡 API Endpoints

### 1. Products (`/api/products`)

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/products` | Retrieve products (supports pagination, price range filters, and search by name) |
| `GET` | `/api/products/{id}` | Get product details by ID |
| `POST` | `/api/products` | Create product (`201 Created` with `Location` header) |
| `PUT` | `/api/products/{id}` | Update product details |
| `DELETE` | `/api/products/{id}` | Delete product by ID (`204 NoContent`) |

### 2. Students (`/api/students`)

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/students` | Retrieve students (supports pagination, age filtering, and search by name) |
| `GET` | `/api/students/{id}` | Get student details by ID |
| `POST` | `/api/students` | Create student record |
| `PUT` | `/api/students/{id}` | Update student record |
| `DELETE` | `/api/students/{id}` | Delete student record |

---

<a id="error-handling"></a>
## 🛡️ Error Response Envelope

All API errors return a uniform JSON schema:

```json
{
  "isSuccess": false,
  "statusCode": 404,
  "errorCode": 1001,
  "message": "Product with id 999 not found."
}
```

---

<a id="faq"></a>
## ❓ FAQ

<details>
<summary><b>1. How do I switch to a production database (SQL Server or PostgreSQL)?</b></summary>
<br/>

Create a DbContext class inheriting from `Microsoft.EntityFrameworkCore.DbContext`, install the appropriate EF Core provider package (`Microsoft.EntityFrameworkCore.SqlServer` or `Npgsql.EntityFrameworkCore.PostgreSQL`), and register `IApplicationDbContext` inside `Program.cs`.
</details>

<details>
<summary><b>2. Is this starter suitable as a mock backend for frontend development?</b></summary>
<br/>

Yes. The repository includes built-in mock data and performs full in-memory CRUD operations out of the box without requiring external database engines.
</details>

---

<a id="license"></a>
## 📄 License

Distributed under the [MIT License](LICENSE). Free for personal and commercial customization.
