using HouseControl.Domain.Entities;
using HouseControl.Infra.Context;
using HouseControl.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HouseControl.Infra.Repositories
{
    public class LancamentoRepository: ILancamentoRepository
    {
        private readonly HouseContext _context;

        public LancamentoRepository(HouseContext context)
        {
            _context = context;
        }

        public async Task<List<Lancamento>> GetAllAsync()
        {
            return await _context.Lancamentos.ToListAsync();
        }

        public async Task<Lancamento> GetByIdAsync(Guid id)
        {
            var lancamento = await _context.Lancamentos.FirstOrDefaultAsync(x => x.Id == id);

            if (lancamento == null)
                throw new Exception("Lançamento não encontrado");

            return lancamento;
        }

        public async Task<Lancamento> CreateLancamentoAsync(Lancamento lancamento)
        {
            await _context.Lancamentos.AddAsync(lancamento);
            await _context.SaveChangesAsync();
            return lancamento;
        }

        public Lancamento UpdateLancamentoAsync(Lancamento lancamento)
        {
            _context.Lancamentos.Update(lancamento);
            _context.SaveChanges();
            return lancamento;
        }

        public async Task<bool> DeleteLancamento(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return false;

            _context.Lancamentos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}