namespace WordPicker.Web.Models;

public class PickHistory
{
    public int Id { get; set; }

    public int WordEntryId { get; set; }

    public int WordListId { get; set; }

    public DateTime PickedAt { get; set; } = DateTime.UtcNow;

    public WordEntry WordEntry { get; set; } = null!;

    public WordList WordList { get; set; } = null!;
}
