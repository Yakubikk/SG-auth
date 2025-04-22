using System.ComponentModel.DataAnnotations;

namespace back_end_9.Models;

public class FileModel
{
    [Key]
    public Guid FileId { get; init; }
    public string FileName { get; init; } = string.Empty;
    public string FilePath { get; init; } = string.Empty;
    public string ContentType { get; init; } = string.Empty;
    public long Size { get; init; }
    public bool IsPublic { get; init; }

    // Связанные короткие ссылки
    public ICollection<ShortUrl> ShortUrls { get; init; } = [];
}