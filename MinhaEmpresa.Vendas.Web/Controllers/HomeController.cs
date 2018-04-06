using System.Web.Mvc;

namespace MinhaEmpresa.Vendas.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Pedidos");
        }

        public ActionResult Pedidos()
        {
            return View();
        }

        public ActionResult NovoPedido()
        {
            return View();
        }
    }
}