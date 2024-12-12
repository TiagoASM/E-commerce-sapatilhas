using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoEcommerceSapatilhas.Pages.Data;
using ProjetoEcommerceSapatilhas.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ProjetoEcommerceSapatilhas.Pages
{
	public class SapatilhaDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _user;

        public Sapatilha Sapatilha { get; set; }
        public List<Sapatilha> Sapatilhas { get; set; }
        public List<decimal> Tamanhos { get; set; }
        public double MediaAvaliacoes { get; set; }

        public List<IdentityUser> UsersList { get; set; }

        [BindProperty]
        public Avaliacao Avaliacao { get; set; }

        [BindProperty]
        public bool VerificacaoAvalicao { get; set; }

        public SapatilhaDetailsModel(ApplicationDbContext context, UserManager<IdentityUser> user)
        {
            _context = context;
            _user = user;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            UsersList = _user.Users.ToList();
            Sapatilhas = await _context.Sapatilhas
                .Where(p => p.ID != id)
                .Take(3)
                .ToListAsync();


            Sapatilha = await _context.Sapatilhas
                .Include(s => s.Avaliacoes)
                .FirstOrDefaultAsync(s => s.ID == id);

            Tamanhos = await _context.Tamanhos
                .Where(t => t.SapatilhaId == id)
                .Select(t => t.TamanhoValor)
                .ToListAsync();

            if(Sapatilha == null)
            {
                return NotFound();
            }

            Sapatilha.NumeroVisualizacoes++;

            await _context.SaveChangesAsync();

            if(Sapatilha.Avaliacoes != null && Sapatilha.Avaliacoes.Any())
            {
                MediaAvaliacoes = Sapatilha.Avaliacoes.Average(a => a.Nota);
            }
            else
            {
                MediaAvaliacoes = 0;
            }

            var userID = _user.GetUserId(User);

            if (_context.Avaliacoes.Any(a => a.Sapatilha.ID == id && a.UserID == userID))
            {
                VerificacaoAvalicao = false;
            }
            else
            {
                VerificacaoAvalicao = true;
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAddRatingAsync(int id, string comentario, int nota)
        {
            var userID = _user.GetUserId(User);

            if(_context.Avaliacoes.Any(a => a.Sapatilha.ID == id && a.UserID == userID))
            {
                ModelState.AddModelError(string.Empty, "Você já avaliou esta sapatilha.");
                return RedirectToPage("/SapatilhaDetails", new { id });
            }
            else
            {
                var novaAvaliacao = new Avaliacao
                {
                    Comentario = comentario,
                    Nota = nota,
                    SapatilhaID = id,
                    UserID = _user.GetUserId(User),
                    DataComentario = DateTime.Now
                };

                _context.Avaliacoes.Add(novaAvaliacao);

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Avaliação adicionada com sucesso!";

                return RedirectToPage("/SapatilhaDetails", new { id });
            }
            


        }
    }
}
