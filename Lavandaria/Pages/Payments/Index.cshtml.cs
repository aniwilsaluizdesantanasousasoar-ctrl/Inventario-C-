using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lavandaria.Pages.Payments;

[Authorize]
public class IndexModel : PageModel
{
    [BindProperty] public string Method { get; set; } = "MB WAY";
    [BindProperty] public decimal Amount { get; set; } = 4.50m;
    public bool Done { get; set; }
    public void OnGet() { }
    public void OnPost() { Done = true; }
}
