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
    public class ComissaoTecnicasController : Controller
    {
        private LigaTabajaraContext db = new LigaTabajaraContext();

        // GET: ComissaoTecnicas
        public ActionResult Index()
        {
            return View(db.ComissaoTecnicas.ToList());
        }

        // GET: ComissaoTecnicas/Details/5
        public ActionResult Details(int? id)
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

        // GET: ComissaoTecnicas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComissaoTecnicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,CargoString,DataNascimento")] ComissaoTecnica comissaoTecnica)
        {
            if (ModelState.IsValid)
            {
                db.ComissaoTecnicas.Add(comissaoTecnica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            return View(comissaoTecnica);
        }

        // POST: ComissaoTecnicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,CargoString,DataNascimento")] ComissaoTecnica comissaoTecnica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comissaoTecnica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
            ComissaoTecnica comissaoTecnica = db.ComissaoTecnicas.Find(id);
            db.ComissaoTecnicas.Remove(comissaoTecnica);
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
