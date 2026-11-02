using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lavandaria.Data;

public class Machine
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Status { get; set; } = "Disponivel"; // Disponivel, Reservada, EmUtilizacao, AguardarRecolha, Manutencao
    public DateTime? ReservedUntil { get; set; }
    public string? ReservedByUserId { get; set; }
    public int? CurrentProgramId { get; set; }
    public DateTime? WashEnd { get; set; }
}

public class WashProgram
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int DurationMinutes { get; set; }
    public decimal Price { get; set; }
}

public class Reservation
{
    public int Id { get; set; }
    public string UserId { get; set; } = "";
    public int MachineId { get; set; }
    public int ProgramId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ConfirmedAt { get; set; }
    public string Status { get; set; } = "Pendente"; // Pendente, Confirmada, Expirada, Cancelada
}

public class QueueEntry
{
    public int Id { get; set; }
    public string UserId { get; set; } = "";
    public DateTime EnqueuedAt { get; set; } = DateTime.UtcNow;
    public bool Notified { get; set; }
    public DateTime? NotifiedAt { get; set; }
}

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<WashProgram> WashPrograms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<QueueEntry> QueueEntries { get; set; }
}
