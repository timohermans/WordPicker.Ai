using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WordPicker.Web.Data;
using WordPicker.Web.Models;

namespace WordPicker.Web.Pages.WordLists;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public WordList? WordList { get; set; }

    [BindProperty]
    public string NewWord { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        WordList = await _context.WordLists
            .Include(w => w.Words.OrderBy(w => w.CreatedAt))
            .FirstOrDefaultAsync(m => m.Id == id);

        if (WordList == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAddWordAsync(int id)
    {
        if (string.IsNullOrWhiteSpace(NewWord))
        {
            return RedirectToPage(new { id });
        }

        var existingWord = await _context.WordEntries
            .FirstOrDefaultAsync(w => w.WordListId == id && w.Value == NewWord);

        if (existingWord != null)
        {
            TempData["ErrorMessage"] = $"'{NewWord}' already exists in this list.";
            return RedirectToPage(new { id });
        }

        var wordEntry = new WordEntry
        {
            WordListId = id,
            Value = NewWord,
            CreatedAt = DateTime.UtcNow
        };

        _context.WordEntries.Add(wordEntry);
        await _context.SaveChangesAsync();

        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostRemoveWordAsync(int id, int wordId)
    {
        var wordEntry = await _context.WordEntries
            .FirstOrDefaultAsync(w => w.Id == wordId && w.WordListId == id);

        if (wordEntry != null)
        {
            _context.WordEntries.Remove(wordEntry);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage(new { id });
    }
}
