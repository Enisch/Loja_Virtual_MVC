using Infra.Data.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Context.Context
{
    public class ContextDb:DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options):base(options)
        {
            
        }
        //DbSet De todas as tabelas do banco;
        public DbSet<Usuario> usuarios { get; set; }
         public DbSet<Produtos> Produtos { get; set; }//Posteriormente acrescentarei imagens na tabela
         public DbSet<ContaUsuario> contaUsuarios { get; set; }
         public DbSet<Categoria_produtos> categorias { get; set; }
         public DbSet<ControleDeVendas> ControleDeVendas { get; set; }


        //DbSet das View's presentes no banco
         public DbSet<ProdutosComCategoria_View> ProdutosCategoria_View { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ModelBuilder da View ProdutosCategoria_View
            modelBuilder.Entity<ProdutosComCategoria_View>().HasNoKey().ToView("ProdutosCategoria_View");

            //modelBuilder.Entity<ProdutosComCategoria_View>(x =>

            //x.ToSqlQuery("Select * from lojavirtual.ProdutosComCategoria_View;")
            //);
            base.OnModelCreating(modelBuilder);
        }
    }
}
