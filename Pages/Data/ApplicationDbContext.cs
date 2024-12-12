using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoEcommerceSapatilhas.Models.Products;

namespace ProjetoEcommerceSapatilhas.Pages.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        //Adicona o dbSetpara as sapatilahs
        public DbSet<Sapatilha> Sapatilhas { get; set; }
        public DbSet<Tamanho> Tamanhos { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração tamanhos
            modelBuilder.Entity<Tamanho>()
                .HasOne(t => t.Sapatilha)
                .WithMany(s => s.Tamanhos)
                .HasForeignKey(t => t.SapatilhaId);

            modelBuilder.Entity<Avaliacao>()
            .HasKey(a => a.ReviewID);

            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Sapatilha)
                .WithMany(s => s.Avaliacoes)
                .HasForeignKey(a => a.SapatilhaID);

            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.User)
                .WithMany() 
                .HasForeignKey(a => a.UserID);
        }

    }
}

