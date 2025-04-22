using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end_9.Models;

public class ShortUrl
{
    [Key]
    public Guid ShortUrlId { get; init; }
    public string Url { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
        
    // Связанный файл
    public Guid FileId { get; init; }
    [ForeignKey("FileId")]
    public FileModel File { get; init; } = null!;
}