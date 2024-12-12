using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjetoEcommerceSapatilhas.Models.Products;

[Table("Sapatilhas")] //Nome da tabela vai ser sapatilhas
public class Sapatilha
{
    [Key] // Define que essa propriedade é a chave primária
    public int ID { get; set; }

    [Required] // Define que o campo é obrigatório (NOT NULL)
    [MaxLength(100)] // Define o tamanho máximo da string para 100 caracteres
    public string Nome { get; set; }

    [MaxLength(1500)] // Define o tamanho máximo da descrição para 1500 caracteres
    public string Descricao { get; set; }

    [Required] // Campo obrigatório
    [MaxLength(50)] // Define o tamanho máximo da marca para 50 caracteres
    public string Marca { get; set; }

    [Column(TypeName = "decimal(18,2)")] // Define que será um decimal com precisão de 18 dígitos e 2 casas decimais
    public decimal Preco { get; set; }

    [Required] // Campo obrigatório
    [MaxLength(500)] // Define o tamanho máximo para a URL ou caminho da imagem
    public string Imagem { get; set; }

    public DateTime DataAdicionado { get; set; }

    [Range(0, int.MaxValue)] // Define que o número de visualizações não pode ser negativo
    public int NumeroVisualizacoes { get; set; }

    public virtual ICollection<Tamanho> Tamanhos { get; set; }
    public virtual ICollection<Avaliacao> Avaliacoes { get; set; }
}
