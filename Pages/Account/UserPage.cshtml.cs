using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoEcommerceSapatilhas.Pages.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Antiforgery;

namespace ProjetoEcommerceSapatilhas.Pages.Account
{
	public class UserPageModel : PageModel
    {

        private readonly ApplicationDbContext _context;


        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public string CurrentEmail { get; set; }
        [BindProperty]
        public string NewEmail { get; set; }
        
        public string StatusMessage { get; set; }

        [BindProperty]
        public string CurrentPhoneNumber { get; set; }
        [BindProperty]
        public string NewPhoneNumber { get; set; }

        [BindProperty]
        public string CurrentPassword { get; set; }
        [BindProperty]
        public string  NewPassword { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }



        public UserPageModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            CurrentEmail = user.Email;
            CurrentPhoneNumber = user.PhoneNumber;

        }

        public async Task<IActionResult> OnPostAsync()
        {
            //Mudar o email
            var user = await _userManager.GetUserAsync(User);


            if (!string.IsNullOrEmpty(NewEmail) && NewEmail != CurrentEmail)
            {
                var checkEmail = await _userManager.FindByEmailAsync(NewEmail);

                if (checkEmail != null)
                {
                    StatusMessage = "Esse email já existe. Tente outro";

                }
                else
                {
                    var tokenEmail = await _userManager.GenerateChangeEmailTokenAsync(user, NewEmail);

                    var resultChangeEmail = await _userManager.ChangeEmailAsync(user, NewEmail, tokenEmail);

                    if (!resultChangeEmail.Succeeded)
                    {
                        foreach (var error in resultChangeEmail.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                    else
                    {
                        StatusMessage = "Alterou o seu email com sucesso!";

                    }
                }
            }

            CurrentEmail = user.Email;

            //Mudar o número de telemovel

            if (!string.IsNullOrEmpty(NewPhoneNumber) && NewPhoneNumber != CurrentPhoneNumber)
            {

                if (NewPhoneNumber.Length == 9)
                {
                    var tokenPhone = await _userManager.GenerateChangePhoneNumberTokenAsync(user, NewPhoneNumber);

                    var resultChangePN = await _userManager.ChangePhoneNumberAsync(user, NewPhoneNumber, tokenPhone);

                    if (!resultChangePN.Succeeded)
                    {
                        foreach (var error in resultChangePN.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                    else
                    {
                        StatusMessage = "Alterou o seu número de telemovel com sucesso!";
                    }


                }
                else
                {
                    StatusMessage = "O número tem que ter 9 digitos";
                }
            }

            CurrentPhoneNumber = user.PhoneNumber;

            //Mudar a password

            if (!string.IsNullOrEmpty(NewPassword) && NewPassword != CurrentPassword)
            {
                if (CurrentPassword != user.PasswordHash)
                {
                    StatusMessage = "Password atual invalida";
                }
                else if (NewPassword != ConfirmPassword)
                {
                    StatusMessage = "As passwords não correspondem";
                }
                else if (NewPassword == user.PasswordHash)
                {
                    StatusMessage = "Tente uma password diferente á atual";
                }
                else
                {

                    var result = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);

                    if (result.Succeeded)
                    {
                        StatusMessage = "A password foi alterada com sucesso";
                    }
                    else
                    {
                        StatusMessage = "Ocurreu um erro ao alterar a password";
                    }
                }
            }

            return Page();
            
        }

    }
} 
