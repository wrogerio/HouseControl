using System.ComponentModel.DataAnnotations;

namespace HouseControl.Domain.Entities;

public class Categoria
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(150, ErrorMessage = "O campo Nome da Categoria pode conter até 150 caracteres")]
    public string Nome { get; set; } = "";

    // relacionamento
    public virtual IEnumerable<Lancamento>? Lancamentos { get; set; }
}
