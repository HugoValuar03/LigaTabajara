using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LigaTabajara.Models;

namespace LigaTabajara.Controllers
{
    public class JogadoresController : Controller
    {
        private LigaTabajaraContext db = new LigaTabajaraContext();

        // GET: Jogadors
        public ActionResult Index()
        {
            var jogadores = db.Jogadores
                .Include(j => j.Time)
                .AsNoTracking() // Melhora performance
                .ToList();

            return View(jogadores);
        }

        // Nos métodos Details, Delete, etc. adicione:
        

        // GET: Jogadors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogador jogador = db.Jogadores.Find(id);
            if (jogador == null)
            {
                return HttpNotFound();
            }
            return View(jogador);
        }

        // GET: Jogadors/Create
        public ActionResult Create()
        {
            PopularDropDowns();
            return View();
        }

        // POST: Jogadors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Jogador jogador)
        {
            if (ModelState.IsValid)
            {
                db.Jogadores.Add(jogador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopularDropDowns(jogador.TimeId);
            return View(jogador);
        }

        // GET: Jogadors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogador jogador = db.Jogadores.Find(id);
            if (jogador == null)
            {
                return HttpNotFound();
            }
            PopularDropDowns(jogador.TimeId);
            return View(jogador);
        }

        private void PopularDropDowns(int? selectedTimeId = null)
        {
            // Lista de Times
            ViewBag.TimeId = new SelectList(db.Times, "Id", "Nome", selectedTimeId);

            // Lista de Pé Preferido
            ViewBag.PePreferidoList = new SelectList(
                Enum.GetValues(typeof(PePreferido))
                    .Cast<PePreferido>()
                    .Select(p => new SelectListItem
                    {
                        Text = p.ToString(),
                        Value = ((int)p).ToString()
                    }), "Value", "Text");

            // Lista de Posições (se necessário)
            ViewBag.PosicaoList = new SelectList(
                Enum.GetValues(typeof(Posicao))
                    .Cast<Posicao>()
                    .Select(p => new SelectListItem
                    {
                        Text = p.ToString(),
                        Value = ((int)p).ToString()
                    }), "Value", "Text");

            var posicaoItems = Enum.GetValues(typeof(Posicao))
                .Cast<Posicao>()
                .Select(p => new {
                    Value = (int)p,
                    Text = GetEnumDescription(p)
                });

                    ViewBag.PosicaoList = new SelectList(posicaoItems, "Value", "Text");
        }

        private string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));
            return attribute != null ? attribute.Name : value.ToString();
        }

        // POST: Jogadors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,DataNascimento,Nacionalidade,Posicao,NumeroCamisa,Altura,Peso,PePreferido,TimeId")] Jogador jogador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jogador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TimeId = new SelectList(db.Times, "Id", "Nome", jogador.TimeId);
            return View(jogador);
        }

        // GET: Jogadors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogador jogador = db.Jogadores.Find(id);
            if (jogador == null)
            {
                return HttpNotFound();
            }
            return View(jogador);
        }

        // POST: Jogadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jogador jogador = db.Jogadores.Find(id);
            db.Jogadores.Remove(jogador);
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
