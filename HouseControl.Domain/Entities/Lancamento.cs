using System.ComponentModel.DataAnnotations;

namespace HouseControl.Domain.Entities;

public class Lancamento
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo Data de Lançamento é obrigatório")]
    public DateTime DtLancamento { get; set; }
    
    [Required]
    [MaxLength(150, ErrorMessage = "O campo Descrição pode conter até 150 caracteres")]
    public string Descricao { get; set; } = "";

    [Required(ErrorMessage = "O campo Valor é obrigatório")]
    [Range(0.01, 9999999999999999.99, ErrorMessage = "O campo Valor deve ser maior que 0,01")]
    public decimal Valor { get; set; }
    public bool IsParcelado { get; set; }

    [Required(ErrorMessage = "O campo Parcela é obrigatório")]
    public int Parcela { get; set; }

    [Required(ErrorMessage = "O campo Total de Parcelas é obrigatório")]
    public int TotalParcelas { get; set; }

    // relacionamento
    [Required(ErrorMessage = "O campo Categoria é obrigatório")]
    public Guid CategoriaId { get; set; }
    public virtual Categoria? Categoria { get; set; }
}