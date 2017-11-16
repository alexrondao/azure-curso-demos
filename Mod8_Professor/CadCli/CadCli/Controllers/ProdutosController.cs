using CadCli.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Controllers
{
    public class ProdutosController:Controller
    {
        private readonly CadCliDataContext _ctx;
        public ProdutosController(CadCliDataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IActionResult> Index()
        {
            //http://www.entityframeworktutorial.net/
            //EagerLoading => carregamento ancioso (JOINS no ToList())
            //LazyLoading => carregamento preguiçoso (vai na base acessa a prop. de navegação)
            //Explicity Loading => não carrega os dados relacionados (EF Core é Default)
            var model = await
                _ctx.Produtos
                    .Include(x=>x.TipoProduto) //Explicity Loading
                .ToListAsync(); 

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }
    }
}
