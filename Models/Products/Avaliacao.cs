using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace ProjetoEcommerceSapatilhas.Models.Products
{
    [Table("Avaliacoes")]
    public class Avaliacao
	{

        [Key]
        public int ReviewID { get; set; } 

        [Required]
        public int SapatilhaID { get; set; } 

        [Required]
        [ForeignKey("SapatilhaID")]
        public virtual Sapatilha Sapatilha { get; set; } 

        [Required]
        public string UserID { get; set; } 

        [ForeignKey("UserID")]
        public virtual IdentityUser User { get; set; } 

        [Required]
        [Range(1, 5, ErrorMessage = "A nota deve estar entre 1 e 5.")]
        public int Nota { get; set; } 

        [Required]
        [MaxLength(500, ErrorMessage = "O comentário pode ter no máximo 500 caracteres.")]
        public string Comentario { get; set; } 

        [Required]
        public DateTime DataComentario { get; set; } 

    }
}

