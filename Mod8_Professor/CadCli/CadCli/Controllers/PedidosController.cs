using CadCli.Data;
using CadCli.Models;
using CadCli.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Controllers
{
    [Route("api/v1/pedidos")]
    public class PedidosController:Controller
    {
        private readonly CadCliDataContext _ctx;
        public PedidosController(CadCliDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var model = await _ctx.Pedidos.ToListAsync();
            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _ctx.Pedidos.FindAsync(id);

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] Pedido pedido)
        {
            _ctx.Pedidos.Add(pedido);
            await _ctx.SaveChangesAsync();

            var service = new QueueService();
            service.Enviar(JsonConvert.SerializeObject(pedido));

            return CreatedAtAction("Get", pedido);
        }



        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }

    }
}
