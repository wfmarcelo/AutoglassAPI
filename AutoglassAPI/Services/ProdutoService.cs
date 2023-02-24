using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoglassAPI.Models;
using AutoglassAPI.Repository;

namespace AutoglassAPI.Services
{
    public class ProdutoService
    {
        private const string INATIVO = "Inativo";

        private ProdutoDAL _produtoDAL;

        public ProdutoService(ProdutoDAL produtoDAL)
        {
            _produtoDAL = produtoDAL;
        }

        public async Task CreateAsync(Produto model)
        {
            
            dateValidation(model);

            await _produtoDAL.CreateAsync(model);
        }

        public async Task UpdateAsync(Produto model)
        {

            dateValidation(model);

            await _produtoDAL.UpdateAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var modelToDelete = await _produtoDAL.GetAsync(id);

            modelToDelete.Situacao = INATIVO;

            await _produtoDAL.UpdateAsync(modelToDelete);
        }

        public async Task<ProdutoDTO> GetAsync(int id)
        {
            var model = await _produtoDAL.GetAsync(id);

            if (model == null)
            {
                return null;
            }
            
            return ProdutoDTO.GetProdutoToProdutoDTO(model);
        }

        public async Task<PaginatedList<ProdutoDTO>> GetAllAsync(string? descricao, int pageNumber = 1, int pageSize = 10)
        {
            return await _produtoDAL.GetAllAsync(descricao, pageNumber, pageSize);
        }

        private static void dateValidation(Produto produto)
        {
            if (produto.DataFabricacao >= produto.DataValidade)
            {
                throw new ArgumentException("A data de fabricação não pode ser maior ou igual a data de validade.");
            }
        }
    }
}