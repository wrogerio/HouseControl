using HouseControl.Domain.Entities;
using HouseControl.Infra.Context;
using HouseControl.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HouseControl.Infra.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly HouseContext _context;

    public CategoriaRepository(HouseContext context)
    {
        _context = context;
    }

    public async Task<List<Categoria>> GetAllAsync()
    {
        return await _context.Categorias.ToListAsync();
    }

    public async Task<Categoria> GetByIdAsync(Guid id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.Id == id);

        if (categoria == null)
            return new Categoria();

        return categoria;
    }

    public async Task<Categoria> GetByNameAsync(string nome)
    {
        var categoria = await _context.Categorias.Where(x => x.Nome == nome).FirstOrDefaultAsync();

        if (categoria == null)
            return new Categoria();

        return categoria;
    }

    public async Task<Categoria> CreateCategoriaAsync(Categoria categoria)
    {
        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public Categoria UpdateCategoriaAsync(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
        _context.SaveChanges();
        return categoria;
    }

    public async Task<bool> DeleteCategoria(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;

        _context.Categorias.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    
}