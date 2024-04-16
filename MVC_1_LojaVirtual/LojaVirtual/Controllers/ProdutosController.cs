using Infra.Data.Domain.Interfaces;
using Infra.Data.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace LojaVirtual.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("Produtos/v1")]
    public class ProdutosController : Controller
    {
        private readonly IProdutosFiltros produtosFiltros;

        public ProdutosController(IProdutosFiltros produtosFiltros)
        {
            this.produtosFiltros = produtosFiltros;
        }


        [HttpGet]
        public async Task<IActionResult> GetProdutos( string? NameProduct, string SortString)//Pagination will be include in the next update.
        {
            //Searcher Bar
            if (!String.IsNullOrEmpty(NameProduct))
                return View(await produtosFiltros.GetProdutosSearch(NameProduct));

            //Filters
            ViewData["NameSortParam"] = String.IsNullOrEmpty(SortString) ? "name_Desc" : "name_asc";
            ViewData["PriceSortParam"] = String.IsNullOrEmpty(SortString) ? "Price_Desc" : "Price_Asc";
            ViewData["StockSortParam"] = String.IsNullOrEmpty(SortString) ? "Stock_Desc" : "Stock_Asc";
            
            switch(SortString)
            {
                case "name_Desc":
                    return View(await produtosFiltros.GetProdutosByName(SortString));

                                case "name_asc":
                                    return View(await produtosFiltros.GetProdutosByName(SortString));

                case "Price_Desc":
                    return View(await produtosFiltros.GetProdutosByPrice(SortString));

                                 case "Price_Asc":
                                     return View(await produtosFiltros.GetProdutosByPrice(SortString));

                case "Stock_Desc":
                    return View(await produtosFiltros.GetProdutosByEstoque(SortString));

                                 case "Stock_Asc":
                                     return View(await produtosFiltros.GetProdutosByEstoque(SortString));

            }
            //Default View
            return View(await produtosFiltros.GetProdutos());
        }

        [HttpGet("Detalhes_Do_Produto")]
        public async Task<IActionResult> GetProdutosDetalhado(int ProdutoId) 
        {
           
            return View(await produtosFiltros.GetProdutosByVIEW(ProdutoId)); 
        }

    }
}
