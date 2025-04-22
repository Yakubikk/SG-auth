using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace back_end_9.Models;

public class UserFile
{
    [Key]
    public Guid UserFileId { get; init; }

    // Связанный пользователь
    public string UserId { get; init; }
    [ForeignKey("UserId")]
    public User User { get; init; } = null!;

    // Связанный файл
    public Guid FileId { get; init; }
    [ForeignKey("FileId")]
    public FileModel File { get; init; } = null!;
}