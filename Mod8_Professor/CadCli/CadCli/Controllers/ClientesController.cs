using CadCli.Data;
using CadCli.Infra;
using CadCli.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Controllers
{
    public class ClientesController : Controller
    {
        private readonly CadCliDataContext _ctx;
        private readonly Keys _keys;

        public ClientesController(CadCliDataContext ctx, IOptions<Keys> keys)
        {
            _ctx = ctx;
            _keys = keys.Value;
        }

        public async Task<IActionResult> Index()
        {
            var model =
                await
                _ctx.Clientes
                    .OrderByDescending(c => c.Id)
                    .Take(_keys.LimiteClientes)
                    .ToListAsync();

            return View(model);
        }

        //public Task<ViewResult> Todos()
        //{
        //    return
        //    Task.Run(()=> {

        //        var model = _ctx.Clientes.ToList();
        //        return View("Index", model);
        //    });

        //}

        public async Task<ViewResult> Todos([FromServices] IDistributedCache cache)
        {
            var cacheKey = "todosClientes";
            var model = JsonConvert.DeserializeObject<List<Cliente>>(await cache.GetStringAsync(cacheKey) ?? "");

            if (model == null || !model.Any())
            {
                model = await _ctx.Clientes.ToListAsync();
                await 
                    cache.SetStringAsync(
                        cacheKey,
                        JsonConvert.SerializeObject(model),
                        new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)  }
                    );
            }


            return View("Index", model);
        }

        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }


    }
}
