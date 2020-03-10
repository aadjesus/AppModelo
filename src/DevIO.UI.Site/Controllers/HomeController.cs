using DevIO.UI.Site.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.UI.Site.Controllers
{
    public class HomeController : Controller
    {
        public readonly IPedidoRepository _pedidoRepository;

        public HomeController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        // Outra forma de fazer uma injeção de dependencia. Obs: Somente para resolver problemas q o contrutor ñ pode ser alterado
        public ActionResult Index([FromServices]IPedidoRepository pedidoRepository) 
        {
            var pedido = pedidoRepository.ObterPedido();

            return View();
        }
    }
}
