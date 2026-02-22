# Model Binding Sources & File Uploading

## Overview

An ASP.NET Core MVC application focused on **model binding sources** and **file handling**. It demonstrates how to bind data from different parts of an HTTP request (`[FromForm]`, `[FromQuery]`, `[BindProperty]`) and how to implement secure file **upload** and **download** functionality.

---

## Learning Objectives

- Understanding the different model binding sources in ASP.NET Core MVC
- Using `[FromForm]`, `[FromQuery]`, and `[BindProperty(SupportsGet = true)]` attributes
- Handling `IFormFile` for multipart file uploads
- Storing uploaded files safely on the server's file system
- Serving files for download using `PhysicalFile` with automatic MIME type detection

---

## Project Structure

```
Model Binding Sources& FileUploading/
└── Model Binding Sources& FileUploading/
    ├── Controllers/
    │   └── HomeController.cs               # Upload and download logic
    ├── Models/
    │   ├── ReportUploadViewModel.cs        # Binding source: [FromForm] / IFormFile
    │   └── ReportSearchViewModel.cs        # Binding source: [FromQuery] / [BindProperty]
    ├── Views/
    │   └── Home/
    │       ├── Index.cshtml                # File upload form
    │       └── Download.cshtml             # File download form
    └── Program.cs
```

---

## Key Concepts

### Model Binding Sources

ASP.NET Core can bind model properties from different locations in the HTTP request. This project demonstrates the following sources:

| Attribute / Annotation          | Binding Source         | Used In                    |
|---------------------------------|------------------------|----------------------------|
| `[FromForm]`                    | Form body (POST)       | `ReportUploadViewModel`    |
| `IFormFile`                     | Multipart form data    | `ReportUploadViewModel`    |
| `[FromQuery(Name = "cat")]`     | Query string `?cat=`   | `ReportSearchViewModel`    |
| `[BindProperty(SupportsGet)]`   | Query string (GET)     | `ReportSearchViewModel`    |

---

### Upload ViewModel — `ReportUploadViewModel`

```csharp
public class ReportUploadViewModel
{
    [Required]
    [StringLength(50)]
    public string ReportTitle { get; set; }

    [Required]
    public IFormFile ReportFile { get; set; }
}
```

`IFormFile` maps directly to the file selected in the `<input type="file">` HTML element.

---

### Search ViewModel — `ReportSearchViewModel`

```csharp
public class ReportSearchViewModel
{
    [BindProperty(Name = "q", SupportsGet = true)]
    public string SearchQuery { get; set; }

    [FromQuery(Name = "cat")]
    public string Category { get; set; }
}
```

Both properties are bound from the URL query string — e.g. `?q=annual&cat=finance`.

---

### Home Controller — `HomeController`

#### File Upload (`POST /Home/Upload`)

1. Validates the `ReportUploadViewModel`.
2. Creates the storage directory `InternalStorage/Reports/` if it does not exist.
3. Generates a unique file name using `Guid.NewGuid()` to prevent overwriting.
4. Streams the uploaded file to disk using `FileStream`.

```csharp
var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ReportFile.FileName);
var fullPath = Path.Combine(_storagePath, fileName);

using var stream = new FileStream(fullPath, FileMode.Create);
await model.ReportFile.CopyToAsync(stream);
```

#### File Download (`GET /Home/Download`)

1. Accepts a `fileName` query parameter.
2. Uses `Path.GetFileName` to sanitize the path and prevent **path traversal attacks**.
3. Detects the correct MIME type using `FileExtensionContentTypeProvider`.
4. Returns a `PhysicalFile` result so the browser downloads the file with the correct content type.

```csharp
var provider = new FileExtensionContentTypeProvider();
if (!provider.TryGetContentType(filePath, out var contentType))
    contentType = "application/octet-stream";

return PhysicalFile(filePath, contentType, safeFileName);
```

---

## Security Notes

- **Path traversal prevention**: `Path.GetFileName(fileName)` strips any directory separators from the user-supplied file name before building the full path.
- **GUID-based file names**: Uploaded files are renamed with a `Guid` to avoid naming collisions and to prevent clients from guessing file names.

---

## Technologies Used

| Technology        | Version |
|-------------------|---------|
| ASP.NET Core MVC  | .NET 10 |
| Bootstrap         | 5.x     |

---

## How to Run

```bash
dotnet run
```

Navigate to `https://localhost:{port}/` to access the upload form, or `https://localhost:{port}/Home/Download` for the download form.
