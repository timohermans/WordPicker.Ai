using System.ComponentModel.DataAnnotations;

namespace WordPicker.Web.Models;

public class WordEntry
{
    public int Id { get; set; }

    public int WordListId { get; set; }

    [Required]
    [StringLength(100)]
    public string Value { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int LastPickCycle { get; set; }

    public WordList WordList { get; set; } = null!;

    public ICollection<PickHistory> PickHistories { get; set; } = new List<PickHistory>();
}
