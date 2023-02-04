using HouseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseControl.Infra.Config;

public class LancamentoConfig : IEntityTypeConfiguration<Lancamento>
{
    public void Configure(EntityTypeBuilder<Lancamento> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Descricao).HasMaxLength(150);

        builder.Property(x => x.TotalParcelas).HasDefaultValue(0);
        builder.Property(x => x.Parcela).HasDefaultValue(0);

        builder.Property(x => x.Valor).HasColumnType("DECIMAL");
    }
}