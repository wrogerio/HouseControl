using HouseControl.Domain.Entities;

namespace HouseControl.Infra.Interfaces;

public interface ICategoriaRepository
{
    public Task<List<Categoria>> GetAllAsync();
    public Task<Categoria> GetByIdAsync(Guid id);
    public Task<Categoria> CreateCategoriaAsync(Categoria categiria);
    public Categoria UpdateCategoriaAsync(Categoria categiria);
    public Task<bool> DeleteCategoria(Guid id);
}