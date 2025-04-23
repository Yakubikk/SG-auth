using back_end_9.Data;
using back_end_9.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[Route("[controller]")]
[ApiController]
public class FilesController(ApplicationDbContext context) : ControllerBase
{
    // GET: Files
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FileModel>>> GetFiles()
    { 
        var files = await context.Files.ToListAsync(); 
        return Ok(files);
    }

    // GET: Files/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FileModel>> GetFile(Guid id)
    {
        var file = await context.Files.FindAsync(id);

        if (file == null)
        {
            return NotFound();
        }

        return Ok(file);
    }

    // GET: Files/User/{userId}
    [HttpGet("User/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<FileModel>>> GetFilesByUserId(Guid userId)
    {
        var userFiles = await context.UserFiles
            .Where(uf => uf.UserId == userId.ToString())
            .Select(uf => uf.File)
            .ToListAsync();

        return Ok(userFiles);
    }

    [Authorize]
    // POST: Files/User/{userId}
    [HttpPost("User/{userId:guid}")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<FileModel>> UploadFile(Guid userId, IFormFile file)
    {
        if (file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var userDirectory = Path.Combine("Uploads", userId.ToString());
        if (!Directory.Exists(userDirectory))
        {
            Directory.CreateDirectory(userDirectory);
        }

        // Получаем оригинальное имя файла и расширение
        var originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
        var extension = Path.GetExtension(file.FileName);
        var fileName = originalFileName + extension;
        var filePath = Path.Combine(userDirectory, fileName);

        // Если файл с таким именем уже существует, добавляем (1), (2) и т.д.
        if (System.IO.File.Exists(filePath))
        {
            var counter = 1;
            do
            {
                fileName = $"{originalFileName}({counter}){extension}";
                filePath = Path.Combine(userDirectory, fileName);
                counter++;
            } while (System.IO.File.Exists(filePath));
        }

        var fileModel = new FileModel
        {
            FileId = Guid.NewGuid(),
            FileName = fileName,
            ContentType = file.ContentType,
            FilePath = filePath,
            Size = file.Length,
            IsPublic = false
        };

        // Save file to disk
        await using (var stream = new FileStream(fileModel.FilePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        context.Files.Add(fileModel);

        var userFile = new UserFile
        {
            UserFileId = Guid.NewGuid(),
            UserId = userId.ToString(),
            FileId = fileModel.FileId
        };

        context.UserFiles.Add(userFile);

        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFile), new { id = fileModel.FileId }, fileModel);
    }

    // DELETE: Files/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteFile(Guid id)
    {
        var fileModel = await context.Files.FindAsync(id);
        if (fileModel == null)
        {
            return NotFound();
        }

        // Delete file from disk
        if (System.IO.File.Exists(fileModel.FilePath))
        {
            System.IO.File.Delete(fileModel.FilePath);
        }

        context.Files.Remove(fileModel);
        await context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpGet("{id:guid}/download")]
    public async Task<IActionResult> DownloadFile(Guid id)
    {
        var fileModel = await context.Files.FindAsync(id);
        if (fileModel == null)
        {
            return NotFound();
        }

        if (!System.IO.File.Exists(fileModel.FilePath))
        {
            return NotFound();
        }

        var fileStream = System.IO.File.OpenRead(fileModel.FilePath);
        return File(fileStream, fileModel.ContentType, fileModel.FileName);
    }
}