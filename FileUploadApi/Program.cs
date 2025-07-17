using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Configure file upload limits
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10 MB limit
});

var app = builder.Build();

// Create uploads directory
const string uploadFolder = "uploads";
Directory.CreateDirectory(uploadFolder);

// Upload endpoint
app.MapPost("/upload", async (IFormFile file) =>
{
    // Validate file size
    if (file.Length == 0)
    {
        return Results.BadRequest("File is empty");
    }

    // Save file
    var filePath = Path.Combine(uploadFolder, file.FileName);
    await using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    return Results.Ok($"File {file.FileName} uploaded successfully");
})
.DisableAntiforgery()
.WithName("UploadFile");

// Download endpoint
app.MapGet("/download/{fileName}", (string fileName) =>
{
    var filePath = Path.Combine(uploadFolder, fileName);

    if (!File.Exists(filePath))
    {
        return Results.NotFound("File not found");
    }

    var fileBytes = File.ReadAllBytes(filePath);
    return Results.File(fileBytes, "application/octet-stream", fileName);
});



app.Run();
