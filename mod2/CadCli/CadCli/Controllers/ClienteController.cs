using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CadCli.Data;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CadCli.Infra;

namespace CadCli.Controllers
{
    public class ClienteController : Controller
    {
        private readonly CadCliDataContext _ctx;
        private readonly Keys _keys;

        public ClienteController(CadCliDataContext ctx, IOptions<Keys> keys)
        {
            _ctx = ctx;
            _keys = keys.Value;
        }

        public IActionResult Index()
        {
            var model = _ctx.Clientes
                .OrderByDescending(c=>c.Id)
                .Take(_keys.LimiteClientes)
                .ToList();

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }
    }
}