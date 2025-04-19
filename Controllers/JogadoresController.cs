using LigaTabajara.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LigaTabajara.Controllers
{
    public class JogadoresController : Controller
    {
        private LigaTabajaraContext db = new LigaTabajaraContext();

        // GET: Jogadores
        public ActionResult Index(string searchPosicao)
        {
            // Carregar os valores do enum Posicao para o dropdown
            var posicoes = Enum.GetValues(typeof(Posicao))
                .Cast<Posicao>()
                .Select(p => new SelectListItem
                {
                    Value = ((int)p).ToString(),
                    Text = p.ToString()
                }).ToList();

            // Adicionar uma opção padrão (opcional)
            posicoes.Insert(0, new SelectListItem { Value = "", Text = "Selecione a Posição" });

            ViewBag.searchPosicao = posicoes;

            // Carregar a lista de jogadores
            var jogadores = db.Jogadores.Include(j => j.Time).AsQueryable();

            // Filtrar por posição, se uma posição foi selecionada
            if (!string.IsNullOrEmpty(searchPosicao) && int.TryParse(searchPosicao, out int posicaoId))
            {
                jogadores = jogadores.Where(j => (int)j.Posicao == posicaoId);
            }

            return View(jogadores.ToList());
        }

        // GET: Jogadores/Create
        public ActionResult Create()
        {
            ViewBag.TimeId = new SelectList(db.Times, "Id", "Nome");
            return View();
        }

        // POST: Jogadores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,DataNascimento,Nacionalidade,Posicao,NumeroCamisa,Altura,Peso,PePreferido,TimeId")] Jogador jogador)
        {
            if (ModelState.IsValid)
            {
                db.Jogadores.Add(jogador);
                db.SaveChanges();

                // Atualizar o status do time associado
                var time = db.Times
                    .Include(t => t.Jogadores)
                    .Include(t => t.ComissaoTecnicas)
                    .SingleOrDefault(t => t.Id == jogador.TimeId);
                if (time != null)
                {
                    time.AtualizarStatus();
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.TimeId = new SelectList(db.Times, "Id", "Nome", jogador.TimeId);
            return View(jogador);
        }

        // GET: Jogadores/Edit/5
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
            ViewBag.TimeId = new SelectList(db.Times, "Id", "Nome", jogador.TimeId);
            return View(jogador);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Jogador jogador = db.Jogadores
                .Include(j => j.Time)
                .Include(j => j.Gols)
                .SingleOrDefault(j => j.Id == id);

            if (jogador == null)
            {
                TempData["ErrorMessage"] = "Jogador não encontrado.";
                return RedirectToAction("Index");
            }

            // Calculate goal statistics
            var totalGols = jogador.Gols.Count(g => !g.Contra);
            var golsContra = jogador.Gols.Count(g => g.Contra);
            var saldoGols = totalGols - golsContra;

            ViewBag.TotalGols = totalGols;
            ViewBag.GolsContra = golsContra;
            ViewBag.SaldoGols = saldoGols;

            return View(jogador);
        }

        // POST: Jogadores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,DataNascimento,Nacionalidade,Posicao,NumeroCamisa,Altura,Peso,PePreferido,TimeId")] Jogador jogador)
        {
            if (ModelState.IsValid)
            {
                var jogadorToUpdate = db.Jogadores
                    .Include(j => j.Time)
                    .SingleOrDefault(j => j.Id == jogador.Id);

                if (jogadorToUpdate == null)
                {
                    return HttpNotFound();
                }

                // Armazenar o TimeId antigo para atualizar o status do time anterior, se necessário
                int? oldTimeId = jogadorToUpdate.TimeId;

                // Atualizar as propriedades manualmente
                jogadorToUpdate.Nome = jogador.Nome;
                jogadorToUpdate.DataNascimento = jogador.DataNascimento;
                jogadorToUpdate.Nacionalidade = jogador.Nacionalidade;
                jogadorToUpdate.Posicao = jogador.Posicao;
                jogadorToUpdate.NumeroCamisa = jogador.NumeroCamisa;
                jogadorToUpdate.Altura = jogador.Altura;
                jogadorToUpdate.Peso = jogador.Peso;
                jogadorToUpdate.PePreferido = jogador.PePreferido;
                jogadorToUpdate.TimeId = jogador.TimeId;

                db.Entry(jogadorToUpdate).State = EntityState.Modified;
                db.SaveChanges();

                // Atualizar o status do time atual
                var timeAtual = db.Times
                    .Include(t => t.Jogadores)
                    .Include(t => t.ComissaoTecnicas)
                    .SingleOrDefault(t => t.Id == jogador.TimeId);
                if (timeAtual != null)
                {
                    timeAtual.AtualizarStatus();
                }

                // Atualizar o status do time anterior, se for diferente
                if (oldTimeId != jogador.TimeId)
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
            ViewBag.TimeId = new SelectList(db.Times, "Id", "Nome", jogador.TimeId);
            return View(jogador);
        }

        // GET: Jogadores/Delete/5
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

        // POST: Jogadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jogador jogador = db.Jogadores
                .Include(j => j.Time)
                .SingleOrDefault(j => j.Id == id);

            if (jogador == null)
            {
                return HttpNotFound();
            }

            int timeId = jogador.TimeId;
            db.Jogadores.Remove(jogador);
            db.SaveChanges();

            // Atualizar o status do time associado
            var time = db.Times
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