using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace ProjetoEcommerceSapatilhas.Pages.Account
{
	public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signinManager;

        public LogoutModel(SignInManager<IdentityUser> signinManager, UserManager<IdentityUser> userManager)
        {
            _signinManager = signinManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _signinManager.SignOutAsync();

            return RedirectToPage("/Index");
        }
    }
}
