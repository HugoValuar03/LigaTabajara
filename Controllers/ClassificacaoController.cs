using LigaTabajara.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LigaTabajara.Controllers
{
    public class ClassificacaoController : Controller
    {
        private LigaTabajaraContext db = new LigaTabajaraContext();

        public ActionResult Index()
        {
            var classificacao = db.Classificacoes
                .Include(t => t.Time)
                .OrderByDescending(t => t.Pontos)
                .ThenByDescending(t => t.SaldoGols)
                .ThenByDescending(t => t.GolsPro)
                .ToList();

            var artilheiros = db.Gols
                .Include(g => g.Jogador)
                .Include(g => g.Jogador.Time)
                .GroupBy(g => g.Jogador)
                .Select(g => new Artilheiros
                {
                    NomeJogador = g.Key.Nome,
                    NomeTime = g.Key.Time.Nome,
                    TotalGols = g.Count(x => !x.Contra)
                })
                .OrderByDescending(a => a.TotalGols)
                .Take(10)
                .ToList();

            ViewBag.Artilheiros = artilheiros;
            return View(classificacao);
        }

        // GET: Classificacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Classificacao classificacao = db.Classificacoes
                .Include(c => c.Time)
                .Include(c => c.Time.Jogadores)
                .Include(c => c.Time.Jogadores.Select(j => j.Gols))
                .SingleOrDefault(c => c.Id == id);

            if (classificacao == null)
            {
                return HttpNotFound();
            }

            // Calcular o saldo de gols do time somando o SaldoGols dos jogadores
            var saldoGolsTime = classificacao.Time?.Jogadores?.Sum(j => j.SaldoGols) ?? 0;
            ViewBag.SaldoGolsTimeJogadores = saldoGolsTime;

            // Listar os jogadores com nome, saldo de gols e total de gols usando Tuple
            var jogadores = classificacao.Time?.Jogadores
                ?.Select(j => new Tuple<string, int, int>(
                    j.Nome,
                    j.Gols?.Count(g => !g.Contra) ?? 0,
                    j.SaldoGols
                ))
                .OrderByDescending(j => j.Item3) // Item3 é o SaldoGols
                .ToList() ?? new List<Tuple<string, int, int>>();
            ViewBag.Jogadores = jogadores;

            return View(classificacao);
        }

        // GET: Classificacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Classificacao classificacao = db.Classificacoes
                .Include(c => c.Time)
                .SingleOrDefault(c => c.Id == id);

            if (classificacao == null)
            {
                return HttpNotFound();
            }

            // Preencher ViewBag com a lista de times para o dropdown
            ViewBag.TimeId = new SelectList(db.Times, "Id", "Nome", classificacao.TimeId);

            return View(classificacao);
        }

        // POST: Classificacao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TimeId,Pontos,Jogos,Vitorias,Empates,Derrotas,GolsMarcados,GolsSofridos")] Classificacao classificacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classificacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Se houver erros, recarregar o dropdown de times
            ViewBag.TimeId = new SelectList(db.Times, "Id", "Nome", classificacao.TimeId);

            return View(classificacao);
        }

        // GET: Classificacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Classificacao classificacao = db.Classificacoes
                .Include(c => c.Time)
                .SingleOrDefault(c => c.Id == id);

            if (classificacao == null)
            {
                return HttpNotFound();
            }

            return View(classificacao);
        }

        // POST: Classificacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Classificacao classificacao = db.Classificacoes.Find(id);
            if (classificacao == null)
            {
                return HttpNotFound();
            }

            db.Classificacoes.Remove(classificacao);
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