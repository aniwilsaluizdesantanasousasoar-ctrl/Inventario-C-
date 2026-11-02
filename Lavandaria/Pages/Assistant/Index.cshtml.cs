using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lavandaria.Pages.Assistant;

[Authorize]
public class IndexModel : PageModel
{
    [BindProperty] public string ClothType { get; set; } = "Normal";
    [BindProperty] public int Pieces { get; set; } = 5;
    [BindProperty] public string Dirt { get; set; } = "Medio";
    public string? Recommendation { get; set; }
    public void OnGet() { }
    public void OnPost()
    {
        if (Dirt == "Alto" || ClothType == "Pesada") Recommendation = "Intensivo (60 min)";
        else if (Pieces <= 3 && Dirt == "Baixo") Recommendation = "Rapido (30 min)";
        else Recommendation = "Normal (45 min)";
    }
}
