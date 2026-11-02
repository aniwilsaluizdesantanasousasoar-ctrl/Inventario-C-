using Lavandaria.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lavandaria.Pages.Reservations;

[Authorize]
public class CreateModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly UserManager<IdentityUser> _um;
    public CreateModel(AppDbContext db, UserManager<IdentityUser> um) { _db = db; _um = um; }
    [BindProperty(SupportsGet = true)] public int MachineId { get; set; }
    [BindProperty] public int ProgramId { get; set; }
    public Machine? Machine { get; set; }
    public List<WashProgram> Programs { get; set; } = new();
    public async Task OnGetAsync()
    {
        Machine = await _db.Machines.FindAsync(MachineId);
        Programs = await _db.WashPrograms.ToListAsync();
        if (Programs.Any()) ProgramId = Programs.First().Id;
    }
    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _um.GetUserAsync(User);
        var m = await _db.Machines.FindAsync(MachineId);
        if (m == null || m.Status != "Disponivel") return RedirectToPage("/Machines/Index");
        m.Status = "Reservada";
        m.ReservedByUserId = user!.Id;
        m.ReservedUntil = DateTime.UtcNow.AddMinutes(15);
        m.CurrentProgramId = ProgramId;
        _db.Reservations.Add(new Reservation { UserId = user.Id, MachineId = MachineId, ProgramId = ProgramId, ConfirmedAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();
        return RedirectToPage("/Machines/Index");
    }
}
