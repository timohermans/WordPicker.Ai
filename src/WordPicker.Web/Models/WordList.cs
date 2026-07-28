using System.ComponentModel.DataAnnotations;

namespace WordPicker.Web.Models;

public class WordList
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public int CurrentPickCycle { get; set; }

    public ICollection<WordEntry> Words { get; set; } = new List<WordEntry>();

    public ICollection<PickHistory> PickHistories { get; set; } = new List<PickHistory>();
}
