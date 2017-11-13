using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CadCli.Data;
using CadCli.Infra;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace CadCli.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly CadCliDataContext _ctx;
        private readonly Keys _keys;

        public ProdutoController(CadCliDataContext ctx, IOptions<Keys> keys)
        {
            _ctx = ctx;
            _keys = keys.Value;
        }

        public IActionResult Index()
        {
            var model = _ctx.Produtos.Include(c => c.Tipo)
                .ToList();

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }
    }
}