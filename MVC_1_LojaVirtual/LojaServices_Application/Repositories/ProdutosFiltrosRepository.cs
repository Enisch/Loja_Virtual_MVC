using Infra.Data.Context.Context;
using Infra.Data.Domain.Interfaces;
using Infra.Data.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace LojaServices_Application.Repositories
{
    public class ProdutosFiltrosRepository : IProdutosFiltros
    {
        private readonly ContextDb db;

        public ProdutosFiltrosRepository(ContextDb db)
        {
            this.db = db;
        }

        //Todos Esses metodos devem ter Paginacao;
        public async Task<List<Produtos>> GetProdutosSearch(string name)
        {
            var ProductByName = await db.Produtos.Where(x => x.Name!.Contains(name)).ToListAsync();//Simula o LIKE da linguagem SQL
            return ProductByName;
        }

        public async Task<List<Produtos>> GetProdutosByPrice(string Price)
        {   
            if(Price == "Price_Desc")
            return await db.Produtos.OrderByDescending(x=> x.ValorDoProduto).ToListAsync();

                return await db.Produtos.OrderBy(x => x.ValorDoProduto).ToListAsync();
        }

        public Task<List<Produtos>> GetProdutosByEstoque(string Stock)
        {
            if(Stock == "Stock_Desc")
            return db.Produtos.OrderByDescending(x=>x.QtdProduto).ToListAsync();

            return db.Produtos.OrderBy(x => x.QtdProduto).ToListAsync();
        }

        public async Task<ProdutosComCategoria_View?> GetProdutosByVIEW(int id)//Os produtos possuiram imagens
        {

            return await db.ProdutosCategoria_View.Where(x => x.idProdutos == id).FirstOrDefaultAsync();
            
        }

        public async Task<List<Produtos>> GetProdutos()
        {
            return await db.Produtos.OrderBy(x=> x.Id).ToListAsync();
        }

        public async Task<List<Produtos>> GetProdutosByName(string Name)
        {
            if(Name== "name_Desc")
                return await db.Produtos.OrderByDescending(x=> x.Name).ToListAsync();


            return await db.Produtos.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
