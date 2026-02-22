# PipelineGuardian

## Overview

An ASP.NET Core MVC application that demonstrates the **request processing pipeline**, including **custom middleware**, **MVC action filters**, **resource filters**, **exception filters**, **Areas**, and **attribute-based routing**.

The project follows a **3-layer (N-Tier) architecture**, separating the application into three projects: **Data**, **BL (Business Logic)**, and the **Web (PipelineGuardian)** project.

---

## Learning Objectives

- Building and registering custom ASP.NET Core **Middleware**
- Understanding **filter types** in ASP.NET Core MVC: Resource, Action, and Exception filters
- Implementing **global filters** registered at application startup
- Using **Areas** to separate application sections (e.g. Admin area)
- Applying **attribute-based routing** with constraints
- Structuring an application with a multi-project (N-Tier) solution

---

## Solution Structure

```
PipelineGuardian/
├── Data/                                          # Data layer — models
│   └── Models/
│       └── BankAccount.cs
│
├── BL/                                            # Business Logic layer — filters & repository
│   ├── Filtters/
│   │   ├── FreezeCheckAttribute.cs                # Resource filter
│   │   ├── GlobalExceptionFilter.cs               # Exception filter (global)
│   │   └── TransactionLogAttribute.cs             # Action filter (audit log)
│   └── Repos/
│       └── BankRepo.cs                            # In-memory BankRepository
│
└── PipelineGuardian/                              # Web layer — MVC app
    ├── Areas/
    │   └── Admin/
    │       ├── Controllers/
    │       │   └── DashboardController.cs         # Admin area controller
    │       └── Views/
    │           └── Dashboard/
    │               └── Index.cshtml
    ├── Controllers/
    │   ├── BankController.cs                      # Bank routes with constraints
    │   └── HomeController.cs
    ├── Middleware/
    │   ├── MaintenanceMiddleware.cs               # 503 when MaintenanceMode = true
    │   └── RequestTimingMiddleware.cs             # Logs request duration
    ├── Views/
    │   └── Bank/
    │       └── Index.cshtml
    └── Program.cs
```

---

## Middleware

### `RequestTimingMiddleware`

Wraps every request in a `Stopwatch` and logs the HTTP method, path, and elapsed time in milliseconds using the built-in `ILogger`.

```
Request GET /balance/1 took 12 ms
```

Registered before `MaintenanceMiddleware` so timing always captures the full pipeline.

---

### `MaintenanceMiddleware`

Reads the `MaintenanceMode` boolean flag from `appsettings.json`. When enabled, it **short-circuits** the pipeline and returns:

```
HTTP 503 Service Unavailable
"The service is currently under maintenance. Please try again later."
```

To toggle maintenance mode, change `appsettings.json`:

```json
{
  "MaintenanceMode": true
}
```

---

## Filters

### `GlobalExceptionFilter` — Exception Filter

Registered globally at startup. Catches any unhandled exception in the MVC pipeline and redirects to the shared `Error.cshtml` view instead of showing a raw error page.

```csharp
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});
```

---

### `FreezeCheckAttribute` — Resource Filter

Applied per-action. Reads the `accountId` route value, looks up the account in `BankRepository`, and **short-circuits** with `HTTP 403 Forbidden` if the account is frozen or does not exist — before any action logic runs.

```
GET /balance/3  →  403 Freezed.   (account Id=3 is frozen)
GET /balance/1  →  200 OK         (account Id=1 is active)
```

---

### `TransactionLogAttribute` — Action Filter

Applied per-action. Runs **after** the action executes successfully (no exception) and writes an audit log entry to the debug output:

```
[Audit Log]: Processed Transaction for Account 5 at 01/01/2026 12:00:00
```

---

## Routing

### Attribute-Based Routes in `BankController`

| Route Pattern                      | Constraint          | Example              |
|------------------------------------|---------------------|----------------------|
| `balance/{accountId:int:min(1)}`   | int, minimum = 1    | `/balance/1`         |
| `bank/user/{username:alpha}`       | letters only        | `/bank/user/Ahmed`   |

---

## Areas

The **Admin** area is registered as a separate routing segment:

```
/Admin/Dashboard/Index
```

The `DashboardController` is decorated with `[Area("Admin")]` and lives under `Areas/Admin/Controllers/`.

---

## Data Layer — `BankAccount`

```csharp
public class BankAccount
{
    public int Id { get; set; }
    public string Owner { get; set; }
    public decimal Balance { get; set; }
    public bool IsFrozen { get; set; }
}
```

---

## In-Memory Repository — `BankRepository`

Registered as a **Singleton** in `Program.cs`. Pre-populated with two accounts:

| Id | Owner  | Balance | IsFrozen |
|----|--------|---------|----------|
| 1  | Ahmed  | 5000    | false    |
| 3  | Hacker | 0       | true     |

---

## Pipeline Order (registered in `Program.cs`)

```
Request
   │
   ▼
UseHttpsRedirection
   │
   ▼
UseRouting
   │
   ▼
RequestTimingMiddleware    ← starts stopwatch
   │
   ▼
MaintenanceMiddleware      ← returns 503 if MaintenanceMode = true
   │
   ▼
UseAuthorization
   │
   ▼
MapControllerRoutes        ← MVC filters run here (FreezeCheck → GlobalException → TransactionLog)
   │
   ▼
Response
```

---

## Technologies Used

| Technology        | Version |
|-------------------|---------|
| ASP.NET Core MVC  | .NET 10 |
| Bootstrap         | 5.x     |

---

## How to Run

```bash
dotnet run --project PipelineGuardian/PipelineGuardian
```

Navigate to `https://localhost:{port}/`.
