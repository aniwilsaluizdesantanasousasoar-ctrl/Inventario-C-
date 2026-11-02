using Lavandaria.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lavandaria.Pages.Queue;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly UserManager<IdentityUser> _um;
    public IndexModel(AppDbContext db, UserManager<IdentityUser> um) { _db = db; _um = um; }
    public List<QueueEntry> Entries { get; set; } = new();
    public int Position { get; set; } = -1;
    public async Task OnGetAsync()
    {
        Entries = await _db.QueueEntries.OrderBy(q => q.EnqueuedAt).ToListAsync();
        var user = await _um.GetUserAsync(User);
        var idx = Entries.FindIndex(q => q.UserId == user!.Id);
        Position = idx >= 0 ? idx + 1 : -1;
    }
    public async Task<IActionResult> OnPostEnqueueAsync()
    {
        var user = await _um.GetUserAsync(User);
        if (!await _db.QueueEntries.AnyAsync(q => q.UserId == user!.Id))
            _db.QueueEntries.Add(new QueueEntry { UserId = user!.Id });
        await _db.SaveChangesAsync();
        return RedirectToPage();
    }
}
