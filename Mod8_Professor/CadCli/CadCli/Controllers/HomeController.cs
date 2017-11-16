using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadCli.Controllers
{
    public class HomeController:Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public void Teste()
        {
            throw new NotImplementedException();
        }

        public IActionResult Erro404()
        {
            return View();
        }


    }
}
