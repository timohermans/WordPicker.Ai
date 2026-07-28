using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WordPicker.Web.Data;
using WordPicker.Web.Models;

namespace WordPicker.Web.Pages.WordLists;

public class PickModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public PickModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<WordList> WordLists { get; set; } = new List<WordList>();

    public List<PickResult> Results { get; set; } = new();

    public bool ShowResults => Results.Count > 0;

    [BindProperty]
    public List<int> SelectedListIds { get; set; } = new();

    public async Task OnGetAsync()
    {
        WordLists = await _context.WordLists
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (SelectedListIds.Count == 0)
        {
            ModelState.AddModelError(string.Empty, "Select at least one list.");
            return Page();
        }

        var wordLists = await _context.WordLists
            .Include(w => w.Words)
            .Where(w => SelectedListIds.Contains(w.Id))
            .OrderByDescending(w => w.CreatedAt)
            .ToDictionaryAsync(w => w.Id);

        WordLists = [.. wordLists.Values];

        foreach (var listId in SelectedListIds)
        {
            if (!wordLists.TryGetValue(listId, out var list))
                continue;

            var eligible = list.Words
                .Where(e => e.WordListId == listId && e.LastPickCycle < list.CurrentPickCycle)
                .ToList();

            if (eligible.Count == 0)
            {
                list.CurrentPickCycle++;
                eligible = [.. list.Words];
            }

            var picked = eligible.OrderBy(_ => Random.Shared.Next()).FirstOrDefault();

            if (picked != null)
            {
                picked.LastPickCycle = list.CurrentPickCycle;

                _context.PickHistories.Add(new PickHistory
                {
                    WordEntryId = picked.Id,
                    WordListId = listId,
                    PickedAt = DateTime.UtcNow
                });
            }

            Results.Add(new PickResult
            {
                ListName = list.Name,
                Word = picked?.Value
            });
        }

        await _context.SaveChangesAsync();

        return Page();
    }

    public class PickResult
    {
        public string ListName { get; set; } = string.Empty;
        public string? Word { get; set; }
    }
}
