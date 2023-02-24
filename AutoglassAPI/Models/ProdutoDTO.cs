

namespace AutoglassAPI.Models
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int? FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }

        public Produto GetProdutoModel(){
            return new Produto {
                Id = this.Id,
                Descricao = this.Descricao,
                Situacao = "Ativo",
                DataFabricacao = this.DataFabricacao,
                DataValidade = this.DataValidade,
                FornecedorId = this.FornecedorId,
                Fornecedor = this.Fornecedor

            };
        }

        public static ProdutoDTO GetProdutoToProdutoDTO(Produto produto)
        {
            return new ProdutoDTO {
                Id = produto.Id,
                Descricao = produto.Descricao,
                DataFabricacao = produto.DataFabricacao,
                DataValidade = produto.DataValidade,
                FornecedorId = produto.FornecedorId,
                Fornecedor = produto.Fornecedor
            };
        }

        public static List<ProdutoDTO> GetProdutosToProdutosDTO(List<Produto> produtos)
        {
            var produtosDTO = new List<ProdutoDTO>();
            
            foreach (var produto in produtos)
            {
                produtosDTO.Add(new ProdutoDTO {
                    Id = produto.Id,
                Descricao = produto.Descricao,
                DataFabricacao = produto.DataFabricacao,
                DataValidade = produto.DataValidade,
                FornecedorId = produto.FornecedorId,
                Fornecedor = produto.Fornecedor
                });
            }

            return produtosDTO;
        }
    }
}