# ASP.NET Core MVC — Learning Tasks

A collection of hands-on projects built while learning **ASP.NET Core MVC** on **.NET 10**. Each project focuses on a specific set of concepts and progressively covers more advanced topics — from basic CRUD to middleware pipelines, validation, and file handling.

---

## Projects

### 1. [`Task1 — Category CRUD`](./Task1/README.md)

> **Concepts:** Entity Framework Core · DbContext · Fluent API · Migrations · CRUD Controller · Tag Helpers

A full CRUD application for managing `Category` records backed by a **SQL Server** database. Covers how to configure EF Core, write migrations, and wire up a complete Create / Read / Update / Delete flow using Razor views.

📁 Path: `Task1/MVCTasks`

---

### 2. [`MVC Model Binding & Validation Lab`](./MVC%20Model%20Binding%20%26%20Validation%20Lab/README.md)

> **Concepts:** Model Binding · Data Annotations · Custom Validation Attribute · IValidatableObject · Remote Validation · ModelState

An employee registration form lab that exercises every layer of ASP.NET Core validation — built-in attributes (`[Required]`, `[Compare]`, `[StringLength]`), a custom `[CompanyEmail]` attribute, object-level validation with `IValidatableObject`, async `[Remote]` username checking, and manual `ModelState.AddModelError`.

📁 Path: `MVC Model Binding & Validation Lab/`

---

### 3. [`Model Binding Sources & File Uploading`](./Model%20Binding%20Sources%26%20FileUploading/README.md)

> **Concepts:** `[FromForm]` · `[FromQuery]` · `[BindProperty]` · `IFormFile` · File Upload · File Download · MIME Detection

Demonstrates the different **model binding sources** in ASP.NET Core and how to handle file uploads using `IFormFile` (saved to `InternalStorage/Reports/`) and serve them back for download using `PhysicalFile` with automatic MIME-type detection. Includes path traversal protection and GUID-based file naming.

📁 Path: `Model Binding Sources& FileUploading/`

---

### 4. [`PipelineGuardian`](./PipelineGuardian/README.md)

> **Concepts:** Custom Middleware · Action Filters · Resource Filters · Exception Filters · Areas · Attribute Routing · N-Tier Architecture

The most advanced project in the collection. Implements a **3-layer solution** (Data / BL / Web) and explores the full ASP.NET Core request pipeline:

- **`RequestTimingMiddleware`** — logs the duration of every request
- **`MaintenanceMiddleware`** — returns HTTP 503 when a config flag is set
- **`GlobalExceptionFilter`** — global exception handler registered at startup
- **`FreezeCheckAttribute`** — resource filter that blocks access to frozen bank accounts
- **`TransactionLogAttribute`** — action filter that writes audit logs after successful actions
- **Admin Area** — separated routing section under `/Admin`

📁 Path: `PipelineGuardian/`

---

## Technologies

| Technology                          | Version  |
|-------------------------------------|----------|
| ASP.NET Core MVC                    | .NET 10  |
| Entity Framework Core (SQL Server)  | 10.0.2   |
| Bootstrap                           | 5.x      |
| jQuery Validation (Unobtrusive)     | 3.x      |

---

## How to Navigate

Each project folder contains its own `README.md` with:
- A description of what the project does
- The full project structure
- Explanation of every key concept with code snippets
- Instructions on how to run the project
