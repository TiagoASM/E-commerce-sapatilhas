using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoEcommerceSapatilhas.Pages.Data;

namespace ProjetoEcommerceSapatilhas.Pages;

public class IndexModel : PageModel
{

    private readonly ApplicationDbContext _context; 

    //Login
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public IdentityUser LoggedInUser { get; set; }

    public IndexModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    //Lista de sapatilhas recentes para exibir na view 
    public List<Sapatilha> SapatilhasRecentes { get; set; }

    public async Task OnGetAsync()
    {
        if(_signInManager.IsSignedIn(User))//Ver se o login isTrue
        {
            LoggedInUser = await _userManager.GetUserAsync(User);
        }

        SapatilhasRecentes = await _context.Sapatilhas
            .OrderByDescending(p => p.DataAdicionado)//Ordena pela data de adicionado
            .Take(5) //Limita a 5 produtos
            .ToListAsync(); //Executa a consulta de forma assincrona
    }
}

