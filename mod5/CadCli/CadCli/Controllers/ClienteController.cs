using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CadCli.Data;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CadCli.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using CadCli.Models;

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

        public async Task<IActionResult> Index()
        {
            var model = await _ctx.Clientes
                .OrderByDescending(c=>c.Id)
                .Take(_keys.LimiteClientes)
                .ToListAsync();

            return View(model);
        }


        public async Task<ViewResult> Todos([FromServices] IDistributedCache cache)
        {
            var cacheKey = "todosClientes";
            var model = JsonConvert.DeserializeObject<List<Cliente>>(await cache.GetStringAsync(cacheKey) ?? "");

            if(model == null || !model.Any())
            {
                model = await _ctx.Clientes.ToListAsync();
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(model),
                    new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });
            }

            return View("Index", model);
        }

        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }
    }
}