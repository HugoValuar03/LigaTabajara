using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LigaTabajara.Models;

namespace LigaTabajara.Controllers
{
    public class TimesController : Controller
    {
        private LigaTabajaraContext db = new LigaTabajaraContext();

        // GET: Times
        public ActionResult Index()
        {
            return View(db.Times.ToList());
        }

        // GET: Times/Details/5
        public ActionResult Details(int? id)
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

        // GET: Times/Create
        public ActionResult Create()
        {
            // Definir o Status como "Inativo" por padrão
            var time = new Time
            {
                Status = "Inativo" // Aqui definimos o valor padrão como "Inativo"
            };

            ViewBag.CoresUniforme = Enum.GetValues(typeof(CorUniforme))
                .Cast<CorUniforme>()
                .Select(c => new SelectListItem
                {
                    Value = ((int)c).ToString(),
                    Text = c.ToString()
                });

            return View(time);
        }

        // POST: Times/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Time time)
        {
            if (ModelState.IsValid)
            {
                db.Times.Add(time);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Repassa a lista em caso de erro
            ViewBag.CoresUniforme = Enum.GetValues(typeof(CorUniforme))
                .Cast<CorUniforme>()
                .Select(c => new SelectListItem
                {
                    Value = ((int)c).ToString(),
                    Text = c.ToString()
                });

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
            return View(time);
        }

        // POST: Times/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Estado,AnoFundacao,Estadio,CapacidadeEstadio,CorPrimariaString,CorSecundariaString,Status")] Time time)
        {
            if (ModelState.IsValid)
            {
                db.Entry(time).State = EntityState.Modified;
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
            Time time = db.Times.Find(id);
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
