using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CadCli.Data;
using Microsoft.EntityFrameworkCore;
using CadCli.Models;

namespace CadCli.Controllers
{
    [Route("api/v1/pedidos")]
    public class PedidoController : Controller
    {
        private readonly CadCliDataContext _ctx;
       

        public PedidoController(CadCliDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var model = await _ctx.Pedidos.ToListAsync();

            if (model == null)
                return NotFound();

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

            return CreatedAtAction("Get", pedido.Id);
        }

        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }
    }
}