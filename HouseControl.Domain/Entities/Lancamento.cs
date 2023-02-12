using System.ComponentModel.DataAnnotations;

namespace HouseControl.Domain.Entities;

public class Lancamento
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo Data de Lançamento é obrigatório")]
    [DataType(DataType.Date, ErrorMessage = "O campo Data de Lançamento deve ser uma data válida")]
    public DateTime DtLancamento { get; set; }

    [MaxLength(200, ErrorMessage = "O campo Descrição deve ter no máximo 200 caracteres")]
    public string Descricao { get; set; } = "";

    [Required(ErrorMessage = "O campo valor é obrigatório")]
    [Range(0.01, 9999999999999999.99, ErrorMessage = "O campo Valor deve ser maior que 0,01")]
    public decimal Valor { get; set; }

    public int Parcela { get; set; }

    public int TotalParcelas { get; set; }

    // relacionamento
    [Required(ErrorMessage = "O campo Categoria é obrigatório")]
    public Guid CategoriaId { get; set; }
    public virtual Categoria? Categoria { get; set; }
}