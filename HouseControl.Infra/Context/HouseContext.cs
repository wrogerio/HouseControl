using HouseControl.Domain.Entities;
using HouseControl.Helper.Constants;
using Microsoft.EntityFrameworkCore;

namespace HouseControl.Infra.Context;

public class HouseContext : DbContext
{
    public HouseContext() { }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Lancamento> Lancamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HouseContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(SharedConstants.VivianDbConnectionString);
    }
}