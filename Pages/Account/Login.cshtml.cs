using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjetoEcommerceSapatilhas.Pages.Shared
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; }

        public class LoginInputModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByEmailAsync(Input.Email); //Vai buscar o email, uma vez que o email é diferente do usario
            if (user != null) // Se o usuário foi encontrado
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, isPersistent: false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return RedirectToPage("/Index");
                    }
                    ModelState.AddModelError(string.Empty, "Login inválido.");
                }
            }
            ModelState.AddModelError(string.Empty, "Introduza um Email valido.");

            
            return Page();
        }
    }
}
