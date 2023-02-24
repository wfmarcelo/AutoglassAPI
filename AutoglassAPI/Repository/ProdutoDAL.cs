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
            var produto = await _context.Produtos
                        .Include(p => p.Fornecedor)
                        .Where(p => p.Situacao == "Ativo")
                        .AsNoTracking()
                        .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
            {
                return null;
            }
            
            return produto;
        }

        public async Task<PaginatedList<ProdutoDTO>> GetAllAsync(string? descricao, int? pageNumber, int pageSize = 10)
        {
            
            
            var produtos = from p in _context.Produtos
                            join f in _context.Fornecedores on p.FornecedorId equals f.Id
                            where p.Situacao == "Ativo"
                            select p;

            if (!String.IsNullOrEmpty(descricao))
            {
                produtos = produtos.Where(p => p.Descricao.ToUpper().Contains(descricao.ToUpper()));
            }
                            
            return await PaginatedList<ProdutoDTO>.CreateAsync(produtos.Include(p => p.Fornecedor).Select(p => ProdutoDTO.GetProdutoToProdutoDTO(p)).AsNoTracking(), pageNumber ?? 1, pageSize);
        }
    }
}