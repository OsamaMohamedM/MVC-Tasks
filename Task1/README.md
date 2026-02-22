# Task 1 — Category CRUD with Entity Framework Core

## Overview

A full-featured ASP.NET Core MVC application that demonstrates how to build a **CRUD (Create, Read, Update, Delete)** interface for a `Category` entity using **Entity Framework Core** with a SQL Server database.

---

## Learning Objectives

- Setting up Entity Framework Core in an ASP.NET Core MVC project
- Defining a `DbContext` and using `IEntityTypeConfiguration` for fluent configuration
- Applying EF Core Migrations to create and manage the database schema
- Building a full CRUD controller with model binding and validation
- Using Razor views with Tag Helpers for forms and navigation

---

## Project Structure

```
Task1/
└── MVCTasks/
    ├── Context/
    │   ├── DbContext.cs                  # AppDbContext — EF Core database context
    │   └── CategoryConfiguration.cs     # Fluent API configuration for Category entity
    ├── Controllers/
    │   └── CategoryController.cs        # Full CRUD controller
    ├── Migrations/
    │   └── 20260131202423_ninit.cs      # Initial EF Core migration
    ├── Models/
    │   └── Category.cs                  # Category entity model
    ├── Views/
    │   └── Category/
    │       ├── Index.cshtml             # List all categories
    │       ├── Create.cshtml            # Create a new category
    │       ├── Edit.cshtml              # Edit an existing category
    │       └── Delete.cshtml            # Delete confirmation page
    └── Program.cs                       # App entry point and service registration
```

---

## Key Concepts

### Entity Model — `Category`

```csharp
public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
```

A simple entity with a `Guid` primary key.

---

### Fluent API Configuration — `CategoryConfiguration`

Uses `IEntityTypeConfiguration<Category>` to configure the table name, primary key, and property constraints without data annotations on the model:

| Property      | Rule                       |
|---------------|----------------------------|
| `Id`          | Primary Key                |
| `Name`        | Required, max length 100   |
| `Description` | Optional, max length 500   |

---

### DbContext — `AppDbContext`

Registers the `Categories` `DbSet` and automatically applies all `IEntityTypeConfiguration` classes found in the assembly via:

```csharp
modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
```

---

### CRUD Controller — `CategoryController`

| Action           | HTTP Method | Route                | Description                        |
|------------------|-------------|----------------------|------------------------------------|
| `Index`          | GET         | `/Category`          | Lists all categories from the DB   |
| `Create`         | GET         | `/Category/Create`   | Renders the create form            |
| `Create`         | POST        | `/Category/Create`   | Saves a new category to the DB     |
| `Edit`           | GET         | `/Category/Edit/{id}`| Renders the edit form              |
| `Edit`           | POST        | `/Category/Edit/{id}`| Updates the category in the DB     |
| `Delete`         | GET         | `/Category/Delete/{id}`| Renders delete confirmation      |
| `DeleteConfirmed`| POST        | `/Category/Delete/{id}`| Removes the category from the DB |

---

### Database Connection

The connection string is read from `appsettings.json` under the key `DefaultConnection`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=...;Database=...;..."
}
```

---

## Technologies Used

| Technology                        | Version  |
|-----------------------------------|----------|
| ASP.NET Core MVC                  | .NET 10  |
| Entity Framework Core (SQL Server)| 10.0.2   |
| Bootstrap                         | 5.x      |

---

## How to Run

1. Update the `DefaultConnection` string in `appsettings.json`.
2. Apply migrations:
   ```bash
   dotnet ef database update
   ```
3. Run the project:
   ```bash
   dotnet run
   ```
4. Navigate to `https://localhost:{port}/Category`.
