using Infra.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Interfaces
{
    public interface IProdutosFiltros
    {
        Task<ProdutosComCategoria_View?> GetProdutosByVIEW(int id);
        Task<List<Produtos>> GetProdutos();
        Task<List<Produtos>> GetProdutosSearch(string name);
        Task<List<Produtos>> GetProdutosByEstoque(string Stock);
        Task<List<Produtos>> GetProdutosByPrice(string Price);
        Task<List<Produtos>> GetProdutosByName(string Name);

    }
}
