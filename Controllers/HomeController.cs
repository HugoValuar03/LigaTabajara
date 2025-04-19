using LigaTabajara.Models;
using System.Linq;
using System.Web.Mvc;

namespace LigaTabajara.Controllers
{
    public class HomeController : Controller
    {
        private LigaTabajaraContext db = new LigaTabajaraContext();

        public ActionResult Index()
        {
            var times = db.Times.ToList();
            ViewBag.LigaApta = IsLigaApta();
            return View(times);
        }

        private bool IsLigaApta()
        {
            var times = db.Times.ToList();
            if (times.Count != 20) return false;

            foreach (var time in times)
            {
                var jogadores = db.Jogadores.Where(j => j.TimeId == time.Id).Count();
                var comissao = db.ComissaoTecnicas.Where(c => c.TimeId == time.Id).GroupBy(c => c.Cargo).Count();
                if (jogadores < 30 || comissao < 5 || time.Status != Status.APTO)
                    return false;
            }
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}