using Lavandaria.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lavandaria.Pages.Machines;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public IndexModel(AppDbContext db) { _db = db; }
    public List<Machine> Machines { get; set; } = new();
    public async Task OnGetAsync() => Machines = await _db.Machines.OrderBy(m => m.Id).ToListAsync();
    public async Task<IActionResult> OnPostPickupAsync(int id)
    {
        var m = await _db.Machines.FindAsync(id);
        if (m != null && m.Status == "AguardarRecolha")
        {
            m.Status = "Disponivel";
            m.ReservedByUserId = null;
            m.CurrentProgramId = null;
            m.WashEnd = null;
            m.ReservedUntil = null;
            await _db.SaveChangesAsync();
        }
        return RedirectToPage();
    }
}
