using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetoEcommerceSapatilhas.Pages.Data;
using Microsoft.AspNetCore.Identity;
using ProjetoEcommerceSapatilhas.Models.Products;

namespace ProjetoEcommerceSapatilhas.Pages.Admin
{
	public class AdminPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminPageModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Sapatilha Sapatilha { get; set; }

        [BindProperty]
        public IFormFile ImagemUpload { get; set; }

        public List<Sapatilha> SapatilhasList { get; set; }

        public List<IdentityUser> Users { get; set; }

        public async Task OnGetAsync(int id)
        {
            SapatilhasList = await _context.Sapatilhas.ToListAsync();

            Users =  _userManager.Users.ToList();

        }

        public async Task<IActionResult> OnPostAsync(string tamanhos)
        {

            try
            {
                if (ImagemUpload != null) //Codigo para a conversao da imagem
                {
                    var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

                    if (!Directory.Exists(uploadDirectory))
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImagemUpload.FileName);
                    var filePath = Path.Combine(uploadDirectory, fileName);
                    
                    
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImagemUpload.CopyToAsync(fileStream);
                    }
                    Sapatilha.Imagem = $"/Images/{fileName}";
                    Console.WriteLine(Sapatilha.Imagem);

                }

                Sapatilha.DataAdicionado = DateTime.Now;
                Sapatilha.NumeroVisualizacoes = 0;

                _context.Sapatilhas.Add(Sapatilha); // Adiciona o produto a base de dados

                await _context.SaveChangesAsync(); // Envia a alteração para base de dados

                if (string.IsNullOrWhiteSpace(tamanhos))
                {
                    ModelState.AddModelError("", "Os tamanhos não podem estar vazios");
                    return Page();
                }
                

                //Separar os tamanhos por virgulas e remover os espaçoes extras
                var tamanhosArray = tamanhos.Split(",")
                                            .Select(t => t.Trim())
                                            .Where(t => !string.IsNullOrWhiteSpace(t))
                                            .ToList();


                //Valdiar cada tamanho como decimal
                var tamanhosValidos = new List<Tamanho>();
                foreach (var tamanhoStr in tamanhosArray)
                {
                    if (decimal.TryParse(tamanhoStr, out var tamanhoDecimal))
                    {
                        try
                        {
                            tamanhosValidos.Add(new Tamanho
                            {
                                SapatilhaId = Sapatilha.ID,
                                TamanhoValor = tamanhoDecimal
                            });
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("Erro: " + ex.Message);
                        }
                         
                    }
                    else
                    {
                        ModelState.AddModelError("", $"O tamanho '{tamanhoStr}' não é válido.");
                        Console.WriteLine("Erro ao tentar converter de string para decimal");
                    }

                }

                _context.Tamanhos.AddRange(tamanhosValidos);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Tamanhos adicionados com sucesso!";
                return RedirectToPage("/Admin/AdminPage");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar a sapatilha : " + ex.Message);
                ModelState.AddModelError("", "Ocurreu um erro ao tentar adicionar o produto.");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostRemoverProdutosAsync(string productIds)
        { 
            Console.WriteLine("IDs dos produtos recebidos:");
            foreach (var id in productIds)
            {
                Console.WriteLine(id);
            }

            var productIdsList = productIds.Split(',')
                .Select(id => id.Trim())
                .Where(id => int.TryParse(id, out _))
                .Select(int.Parse)
                .ToList();

            if (productIdsList == null || !productIdsList.Any())
            {
                return BadRequest("Nenhum produto foi selecionado");
            }

            try
            {
                var ids = string.Join(",", productIdsList);
                var query = $"SELECT * FROM Sapatilhas WHERE ID IN ({ids})";

                var produtosRemover = await _context.Sapatilhas
                    .FromSqlRaw(query)
                    .ToListAsync();
                    
               
                _context.Sapatilhas.RemoveRange(produtosRemover);
                await _context.SaveChangesAsync();

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Erro ao remover as sapatilhas ");
            }
        }

        public async Task OnPostRemoverUtilizadoresAsync(string utilizadoresIds)
        {

            if(string.IsNullOrEmpty(utilizadoresIds))
            {
                Console.WriteLine("Nenhum utilizador foi recebido");
            }

            var ids = utilizadoresIds.Split(',');

            try
            {
                Console.WriteLine("Lista de produtos recebidos : ");
                foreach (var utilizadorId in ids)
                {
                    Console.WriteLine(utilizadorId);
                    await _userManager.DeleteAsync(await _userManager.FindByIdAsync(utilizadorId));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

       
    }
}
