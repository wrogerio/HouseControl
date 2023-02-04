using HouseControl.Domain.Entities;

namespace HouseControl.Infra.Interfaces;

public interface ILancamentoRepository
{
    public Task<List<Lancamento>> GetAllAsync();
    public Task<Lancamento> GetByIdAsync(Guid id);
    public Task<Lancamento> CreateLancamentoAsync(Lancamento lancamento);
    public Lancamento UpdateLancamentoAsync(Lancamento lancamento);
    public Task<bool> DeleteLancamento(Guid id);
}