using LigaTabajara.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LigaTabajara.Controllers
{
    public class ComissaoTecnicasController : Controller
    {
        private LigaTabajaraContext db = new LigaTabajaraContext();

        // GET: ComissaoTecnicas
        public ActionResult Index(string searchCargo)
        {
            var cargos = Enum.GetValues(typeof(Cargo))
                .Cast<Cargo>()
                .Select(c => new SelectListItem
                {
                    Value = ((int)c).ToString(),
                    Text = c.GetType()
                        .GetMember(c.ToString())
                        .First()
                        .GetCustomAttributes(typeof(DisplayAttribute), false)
                        .Cast<DisplayAttribute>()
                        .FirstOrDefault()?.Name ?? c.ToString()
                }).ToList();

            cargos.Insert(0, new SelectListItem { Value = "", Text = "Todos" });

            ViewBag.CargoList = cargos;

            var comissaoTecnicas = db.ComissaoTecnicas.Include(c => c.Time).AsQueryable();

            if (!string.IsNullOrEmpty(searchCargo) && int.TryParse(searchCargo, out int cargoId))
            {
                comissaoTecnicas = comissaoTecnicas.Where(c => (int)c.Cargo == cargoId);
            }

            return View(comissaoTecnicas.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ComissaoTecnica comissaoTecnica = db.ComissaoTecnicas
                .Include(c => c.Time)
                .SingleOrDefault(c => c.Id == id);

            if (comissaoTecnica == null)
            {
                return HttpNotFound();
            }

            return View(comissaoTecnica);
        }

        // GET: ComissaoTecnicas/Create
        public ActionResult Create()
        {
            ViewBag.TimeId = new SelectList(db.Times, "Id", "Nome");
            return View();
        }

        // POST: ComissaoTecnicas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Cargo,DataNascimento,TimeId")] ComissaoTecnica comissaoTecnica)
        {
            if (ModelState.IsValid)
            {
                db.ComissaoTecnicas.Add(comissaoTecnica);
                db.SaveChanges();

                // Atualizar o status do time associado
                var time = db.Times
                    .Include(t => t.Jogadores)
                    .Include(t => t.ComissaoTecnicas)
                    .SingleOrDefault(t => t.Id == comissaoTecnica.TimeId);
                if (time != null)
                {
                    time.AtualizarStatus();
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.TimeId = new SelectList(db.Times, "Id", "Nome", comissaoTecnica.TimeId);
            return View(comissaoTecnica);
        }

        // GET: ComissaoTecnicas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComissaoTecnica comissaoTecnica = db.ComissaoTecnicas.Find(id);
            if (comissaoTecnica == null)
            {
                return HttpNotFound();
            }
            ViewBag.TimeId = new SelectList(db.Times, "Id", "Nome", comissaoTecnica.TimeId);
            return View(comissaoTecnica);
        }

        // POST: ComissaoTecnicas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Cargo,DataNascimento,TimeId")] ComissaoTecnica comissaoTecnica)
        {
            if (ModelState.IsValid)
            {
                var comissaoToUpdate = db.ComissaoTecnicas
                    .Include(c => c.Time)
                    .SingleOrDefault(c => c.Id == comissaoTecnica.Id);

                if (comissaoToUpdate == null)
                {
                    return HttpNotFound();
                }

                // Armazenar o TimeId antigo para atualizar o status do time anterior, se necessário
                int? oldTimeId = comissaoToUpdate.TimeId;

                // Atualizar as propriedades manualmente
                comissaoToUpdate.Nome = comissaoTecnica.Nome;
                comissaoToUpdate.Cargo = comissaoTecnica.Cargo;
                comissaoToUpdate.DataNascimento = comissaoTecnica.DataNascimento;
                comissaoToUpdate.TimeId = comissaoTecnica.TimeId;

                db.Entry(comissaoToUpdate).State = EntityState.Modified;
                db.SaveChanges();

                // Atualizar o status do time atual
                var timeAtual = db.Times
                    .Include(t => t.Jogadores)
                    .Include(t => t.ComissaoTecnicas)
                    .SingleOrDefault(t => t.Id == comissaoTecnica.TimeId);
                if (timeAtual != null)
                {
                    timeAtual.AtualizarStatus();
                }

                // Atualizar o status do time anterior, se for diferente
                if (oldTimeId != comissaoTecnica.TimeId)
                {
                    var timeAntigo = db.Times
                        .Include(t => t.Jogadores)
                        .Include(t => t.ComissaoTecnicas)
                        .SingleOrDefault(t => t.Id == oldTimeId);
                    if (timeAntigo != null)
                    {
                        timeAntigo.AtualizarStatus();
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TimeId = new SelectList(db.Times, "Id", "Nome", comissaoTecnica.TimeId);
            return View(comissaoTecnica);
        }

        // GET: ComissaoTecnicas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComissaoTecnica comissaoTecnica = db.ComissaoTecnicas.Find(id);
            if (comissaoTecnica == null)
            {
                return HttpNotFound();
            }
            return View(comissaoTecnica);
        }

        // POST: ComissaoTecnicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComissaoTecnica comissaoTecnica = db.ComissaoTecnicas
                .Include(c => c.Time)
                .SingleOrDefault(c => c.Id == id);

            if (comissaoTecnica == null)
            {
                return HttpNotFound();
            }

            int timeId = comissaoTecnica.TimeId;
            db.ComissaoTecnicas.Remove(comissaoTecnica);
            db.SaveChanges();

            // Atualizar o status do time associado
            var time = db.Times
                .Include(t => t.Nome)
                .Include(t => t.Jogadores)
                .Include(t => t.ComissaoTecnicas)
                .SingleOrDefault(t => t.Id == timeId);
            if (time != null)
            {
                time.AtualizarStatus();
                db.SaveChanges();
            }

            return RedirectToAction("Index");
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