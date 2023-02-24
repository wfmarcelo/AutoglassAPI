using AutoglassAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoglassAPI.Repository
{
    public class ProdutoDAL
    {
        private readonly AutoglassContext _context;

        public ProdutoDAL(AutoglassContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Produto model)
        {
            await _context.Produtos.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Produto model)
        {
            _context.Produtos.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task<Produto> GetAsync(int id)
        {
            return await _context.Produtos
                        .Include(p => p.Fornecedor)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IList<Produto>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.Produtos
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        }
    }
}