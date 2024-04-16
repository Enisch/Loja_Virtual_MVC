using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Models
{
    
    public class ProdutosComCategoria_View
    {
        
        public int idProdutos { get; set; }
        
        public string? NomeProduto { get; set; }
        
        public double ValorProduto { get; set; }
        
        public int QtdProduto { get; set; }
        
        public string? NomeCategoria { get; set; }
        public string? Img { get; set; }

    }
}
