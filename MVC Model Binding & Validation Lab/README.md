# MVC Model Binding & Validation Lab

## Overview

An ASP.NET Core MVC application that serves as a hands-on lab for exploring **model binding**, **data annotations**, **custom validation attributes**, **IValidatableObject**, and **remote (async) validation**.

The main feature is an **Employee Registration Form** that demonstrates multiple layers of server-side and client-side validation working together.

---

## Learning Objectives

- Using `[BindProperty]` on a controller property to bind form data
- Applying built-in Data Annotation attributes (`[Required]`, `[EmailAddress]`, `[StringLength]`, `[Compare]`, `[DataType]`)
- Creating a **custom validation attribute** by inheriting from `ValidationAttribute`
- Implementing **`IValidatableObject`** for cross-property (object-level) validation
- Using `[Remote]` for **asynchronous server-side validation** called from the client via jQuery Unobtrusive Validation
- Adding **manual model errors** with `ModelState.AddModelError`

---

## Project Structure

```
MVC Model Binding & Validation Lab/
└── MVC Model Binding & Validation Lab/
    ├── Controllers/
    │   ├── AccountController.cs          # Registration form controller
    │   └── ValidationController.cs       # Remote validation endpoint
    ├── Models/
    │   └── EmployeeRegisterViewModel.cs  # ViewModel with all validation rules
    ├── Validation/
    │   └── CompanyEmailAttribute.cs      # Custom validation attribute
    ├── Views/
    │   └── Account/
    │       └── Register.cshtml           # Registration form view
    └── Program.cs
```

---

## Key Concepts

### ViewModel — `EmployeeRegisterViewModel`

Implements `IValidatableObject` and is decorated with multiple validation attributes:

| Property          | Validation Rules                                                      |
|-------------------|-----------------------------------------------------------------------|
| `Username`        | `[Required]`, `[Remote]` — async uniqueness check                    |
| `Email`           | `[Required]`, `[EmailAddress]`, `[CompanyEmail]` — custom attribute  |
| `Password`        | `[Required]`, `[StringLength(100, MinimumLength = 8)]`               |
| `ConfirmPassword` | `[Compare("Password")]` — must match Password                        |
| `JoinDate`        | `[Required]`, `IValidatableObject` — must not be a past date         |

---

### Custom Validation Attribute — `CompanyEmailAttribute`

Inherits from `ValidationAttribute` and overrides `IsValid`. It enforces that the email address ends with `@company.com`:

```csharp
if (email.EndsWith("@company.com", StringComparison.OrdinalIgnoreCase))
    return ValidationResult.Success;

return new ValidationResult("it must end with @company.com");
```

---

### IValidatableObject — Cross-Property Validation

The `Validate` method checks that `JoinDate` is not in the past. This validation runs **after** all individual property attributes pass:

```csharp
if (JoinDate < DateTime.Today)
{
    yield return new ValidationResult("invalid join date", new[] { nameof(JoinDate) });
}
```

---

### Remote Validation — `ValidationController`

Provides a JSON endpoint for client-side async username uniqueness checking via `[Remote]`:

```csharp
[AcceptVerbs("GET", "POST")]
public IActionResult Index(string username)
{
    var takenNames = new[] { "admin", "osos", "root" };
    if (takenNames.Contains(username.ToLower()))
        return Json("Username is already taken");
    return Json(true);
}
```

The browser calls this endpoint on blur without a full page reload.

---

### Account Controller — `AccountController`

Uses `[BindProperty]` on the `Input` property so the model is bound automatically on POST without needing a method parameter:

```csharp
[BindProperty]
public EmployeeRegisterViewModel Input { get; set; }
```

It also adds a **manual model error** to block emails containing the word "forbidden":

```csharp
if (Input.Email.Contains("forbidden"))
    ModelState.AddModelError("Input.Email", "Email is blocked");
```

---

## Validation Flow

```
HTTP POST /Account/Register
        │
        ▼
[BindProperty] fills Input
        │
        ▼
Data Annotations checked  ──► [Required], [EmailAddress], [CompanyEmail], [Compare] ...
        │
        ▼
IValidatableObject.Validate()  ──► JoinDate check
        │
        ▼
Manual ModelState.AddModelError()  ──► "forbidden" email check
        │
        ▼
ModelState.IsValid?
    ├─ No  ──► return View(Input) with error messages
    └─ Yes ──► RedirectToAction("Success")
```

---

## Technologies Used

| Technology             | Version |
|------------------------|---------|
| ASP.NET Core MVC       | .NET 10 |
| jQuery Validation      | 3.x     |
| jQuery Unobtrusive Validation | latest |
| Bootstrap              | 5.x     |

---

## How to Run

```bash
dotnet run
```

Navigate to `https://localhost:{port}/Account/Register`.
