using LigaTabajara.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LigaTabajara.Controllers
{
    public class TimesController : Controller
    {
        private LigaTabajaraContext db = new LigaTabajaraContext();

        // GET: Times
        public ActionResult Index()
        {
            var times = db.Times.Include(t => t.Jogadores).Include(t => t.ComissaoTecnicas).ToList();
            return View(times);
        }

        // GET: Times/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Time time = db.Times
                .Include(t => t.Jogadores)           // Include Jogadores
                .Include(t => t.ComissaoTecnicas)    // Include ComissaoTecnicas
                .FirstOrDefault(t => t.Id == id);    // Use FirstOrDefault instead of Find
            if (time == null)
            {
                return HttpNotFound();
            }
            return View(time);
        }

        // GET: Times/Create
        public ActionResult Create()
        {
            // Populate ViewBag.CorUniformeList with the enum values
            ViewBag.CorUniformeList = new SelectList(
                Enum.GetValues(typeof(CorUniforme)).Cast<CorUniforme>().Select(c => new SelectListItem
                {
                    Value = ((int)c).ToString(),
                    Text = c.ToString()
                }),
                "Value",
                "Text"
            );

            return View();
        }

        // POST: Times/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Estado,AnoFundacao,Estadio,CapacidadeEstadio,CorUniforme,Status")] Time time)
        {
            if (ModelState.IsValid)
            {
                time.Jogadores = new List<Jogador>(); // Inicializa as coleções
                time.ComissaoTecnicas = new List<ComissaoTecnica>();
                time.AtualizarStatus(); // Atualiza o status antes de salvar
                db.Times.Add(time);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(time);
        }

        // GET: Times/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Time time = db.Times.Find(id);
            if (time == null)
            {
                return HttpNotFound();
            }

            // Handle invalid CorUniforme values (e.g., 0)
            if ((int)time.CorUniforme == 0)
            {
                time.CorUniforme = CorUniforme.PRIMARIA; // Default to PRIMARIA
            }

            // Populate ViewBag.CorUniformeList with the enum values
            ViewBag.CorUniformeList = new SelectList(
                Enum.GetValues(typeof(CorUniforme)).Cast<CorUniforme>().Select(c => new SelectListItem
                {
                    Value = ((int)c).ToString(),
                    Text = c.ToString()
                }),
                "Value",
                "Text",
                (int)time.CorUniforme
            );

            return View(time);
        }

        // POST: Times/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Estado,AnoFundacao,Estadio,CapacidadeEstadio,CorUniforme,Status")] Time time)
        {
            if (ModelState.IsValid)
            {
                var timeToUpdate = db.Times
                    .Include(t => t.Jogadores)
                    .Include(t => t.ComissaoTecnicas)
                    .SingleOrDefault(t => t.Id == time.Id);

                if (timeToUpdate == null)
                {
                    return HttpNotFound();
                }

                // Atualizar as propriedades manualmente
                timeToUpdate.Nome = time.Nome;
                timeToUpdate.Estado = time.Estado;
                timeToUpdate.AnoFundacao = time.AnoFundacao;
                timeToUpdate.Estadio = time.Estadio;
                timeToUpdate.CapacidadeEstadio = time.CapacidadeEstadio;
                timeToUpdate.CorUniforme = time.CorUniforme;
                timeToUpdate.AtualizarStatus(); // Atualiza o status antes de salvar

                db.Entry(timeToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(time);
        }

        // GET: Times/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Time time = db.Times.Find(id);
            if (time == null)
            {
                return HttpNotFound();
            }
            return View(time);
        }

        // POST: Times/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Time time = db.Times
                .Include(t => t.Jogadores)
                .Include(t => t.ComissaoTecnicas)
                .SingleOrDefault(t => t.Id == id);

            if (time == null)
            {
                return HttpNotFound();
            }

            db.Times.Remove(time);
            db.SaveChanges();
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