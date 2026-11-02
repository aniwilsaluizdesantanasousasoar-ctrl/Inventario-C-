using Lavandaria.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite("Data Source=lavandaria.db"));
builder.Services.AddDefaultIdentity<IdentityUser>(o => { o.SignIn.RequireConfirmedAccount = false; })
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    if (!db.Machines.Any())
    {
        for (int i = 1; i <= 4; i++)
            db.Machines.Add(new Machine { Name = $"Maquina {i}" });
        db.WashPrograms.AddRange(
            new WashProgram { Name = "Rapido", DurationMinutes = 30, Price = 3.50m },
            new WashProgram { Name = "Normal", DurationMinutes = 45, Price = 4.50m },
            new WashProgram { Name = "Intensivo", DurationMinutes = 60, Price = 5.50m });
        db.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
