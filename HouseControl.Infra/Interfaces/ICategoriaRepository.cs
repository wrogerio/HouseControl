using HouseControl.Domain.Entities;

namespace HouseControl.Infra.Interfaces;

public interface ICategoriaRepository
{
    public Task<List<Categoria>> GetAllAsync();
    public Task<Categoria> GetByIdAsync(Guid id);
    public Task<Categoria> GetByNameAsync(string nome);
    public Task<Categoria> CreateCategoriaAsync(Categoria categoria);
    public Categoria UpdateCategoriaAsync(Categoria categoria);
    public Task<bool> DeleteCategoria(Guid id);
}