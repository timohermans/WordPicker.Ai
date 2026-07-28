using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WordPicker.Web.Data;
using WordPicker.Web.Models;

namespace WordPicker.Web.Pages.WordLists;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<WordList> WordLists { get; set; } = new List<WordList>();

    [BindProperty]
    public WordList NewWordList { get; set; } = new();

    public async Task OnGetAsync()
    {
        WordLists = await _context.WordLists
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            WordLists = await _context.WordLists
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync();
            return Page();
        }

        NewWordList.CreatedAt = DateTime.UtcNow;
        NewWordList.UpdatedAt = DateTime.UtcNow;
        _context.WordLists.Add(NewWordList);
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }
}
