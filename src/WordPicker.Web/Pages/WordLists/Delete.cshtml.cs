using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WordPicker.Web.Data;
using WordPicker.Web.Models;

namespace WordPicker.Web.Pages.WordLists;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public WordList? WordList { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        WordList = await _context.WordLists
            .FirstOrDefaultAsync(m => m.Id == id);

        if (WordList == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var wordList = await _context.WordLists.FindAsync(id);
        if (wordList != null)
        {
            _context.WordLists.Remove(wordList);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
