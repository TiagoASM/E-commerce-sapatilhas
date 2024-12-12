using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoEcommerceSapatilhas.Models.Products
{
	[Table("Tamanhos")]
	public class Tamanho
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int SapatilhaId { get; set; }

		[Required]
		[Range(35,43)]
		public decimal TamanhoValor { get; set; }

		[ForeignKey("SapatilhaId")]
		public virtual Sapatilha Sapatilha { get; set; }
	}
}

