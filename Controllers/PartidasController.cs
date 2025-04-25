using LigaTabajara.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LigaTabajara.Controllers
{
    public class PartidasController : Controller
    {
        private LigaTabajaraContext db = new LigaTabajaraContext();

        public ActionResult Index(string searchEstadio, int? searchRodada)
        {
            var partidas = db.Partidas
                .Include(p => p.TimeCasa)
                .Include(p => p.TimeFora)
                .OrderBy(p => p.Rodada)
                .ThenBy(p => p.DataHora);

            if (!string.IsNullOrEmpty(searchEstadio))
                partidas = (IOrderedQueryable<Partida>)partidas.Where(p => p.Estadio.Contains(searchEstadio));

            if (searchRodada.HasValue)
                partidas = (IOrderedQueryable<Partida>)partidas.Where(p => p.Rodada == searchRodada.Value);

            ViewBag.Rodadas = new SelectList(Enumerable.Range(1, 38));
            return View(partidas.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Partida partida = db.Partidas
                .Include(p => p.TimeCasa)
                .Include(p => p.TimeFora)
                .Include(p => p.Gols)
                .Include(p => p.Gols.Select(g => g.Jogador))
                .SingleOrDefault(p => p.Id == id);

            if (partida == null)
            {
                return HttpNotFound();
            }

            return View(partida);
        }

        public ActionResult Create()
        {
            ViewBag.TimeCasaId = new SelectList(db.Times, "Id", "Nome");
            ViewBag.TimeForaId = new SelectList(db.Times, "Id", "Nome");
            ViewBag.Rodadas = new SelectList(Enumerable.Range(1, 38));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Partida partida, List<int> jogadorId, List<int> minuto, List<string> tipoGol, List<bool> contra)
        {
            if (ModelState.IsValid)
            {
                db.Partidas.Add(partida);
                db.SaveChanges();

                int placarCasa = 0, placarFora = 0;

                if (jogadorId != null && jogadorId.Count > 0)
                {
                    for (int i = 0; i < jogadorId.Count; i++)
                    {
                        if (minuto == null || tipoGol == null || contra == null || i >= minuto.Count || i >= tipoGol.Count || i >= contra.Count)
                        {
                            ModelState.AddModelError("", "Dados de gols inconsistentes.");
                            continue;
                        }

                        var jogador = db.Jogadores.Find(jogadorId[i]);
                        if (jogador == null || (jogador.TimeId != partida.TimeCasaId && jogador.TimeId != partida.TimeForaId))
                        {
                            ModelState.AddModelError("", $"Jogador inválido: ID {jogadorId[i]}.");
                            continue;
                        }

                        bool isGolCasa = (jogador.TimeId == partida.TimeCasaId && !contra[i]) || (jogador.TimeId == partida.TimeForaId && contra[i]);
                        if (isGolCasa) placarCasa++;
                        else placarFora++;

                        db.Gols.Add(new Gol
                        {
                            PartidaId = partida.Id,
                            JogadorId = jogadorId[i],
                            Minuto = minuto[i],
                            TipoGol = tipoGol[i],
                            Contra = contra[i]
                        });

                        // Atualizar o SaldoGols do jogador
                        var golsFavor = db.Gols.Count(g => g.JogadorId == jogador.Id && !g.Contra);
                        var golsContra = db.Gols.Count(g => g.JogadorId == jogador.Id && g.Contra);
                        jogador.SaldoGols = golsFavor - golsContra;
                    }

                    if (ModelState.IsValid)
                    {
                        partida.PlacarCasa = placarCasa;
                        partida.PlacarFora = placarFora;
                        db.Entry(partida).State = EntityState.Modified;
                        db.SaveChanges();
                        AtualizarClassificacao(partida);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.TimeCasaId = new SelectList(db.Times, "Id", "Nome", partida.TimeCasaId);
            ViewBag.TimeForaId = new SelectList(db.Times, "Id", "Nome", partida.TimeForaId);
            ViewBag.Rodadas = new SelectList(Enumerable.Range(1, 38), partida.Rodada);
            return View(partida);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var partida = db.Partidas.Find(id);
            if (partida == null)
                return HttpNotFound();

            ViewBag.TimeCasaId = new SelectList(db.Times, "Id", "Nome", partida.TimeCasaId);
            ViewBag.TimeForaId = new SelectList(db.Times, "Id", "Nome", partida.TimeForaId);
            ViewBag.Rodadas = new SelectList(Enumerable.Range(1, 38), partida.Rodada);
            return View(partida);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Partida partida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partida).State = EntityState.Modified;
                db.SaveChanges();
                AtualizarClassificacao(partida);
                return RedirectToAction("Index");
            }

            ViewBag.TimeCasaId = new SelectList(db.Times, "Id", "Nome", partida.TimeCasaId);
            ViewBag.TimeForaId = new SelectList(db.Times, "Id", "Nome", partida.TimeForaId);
            ViewBag.Rodadas = new SelectList(Enumerable.Range(1, 38), partida.Rodada);
            return View(partida);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Partida partida = db.Partidas
                .Include(p => p.TimeCasa)
                .Include(p => p.TimeFora)
                .Include(p => p.Gols)
                .Include(p => p.Gols.Select(g => g.Jogador))
                .SingleOrDefault(p => p.Id == id);

            return View(partida);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var partida = db.Partidas.Find(id);
            var gols = db.Gols.Where(g => g.PartidaId == id).ToList();
            var jogadoresAfetados = gols.Select(g => g.JogadorId).Distinct().ToList();

            db.Gols.RemoveRange(gols);
            db.Partidas.Remove(partida);
            db.SaveChanges();

            // Recalcular o SaldoGols dos jogadores afetados
            foreach (var jogadorId in jogadoresAfetados)
            {
                var jogador = db.Jogadores.Find(jogadorId);
                if (jogador != null)
                {
                    var golsFavor = db.Gols.Count(g => g.JogadorId == jogador.Id && !g.Contra);
                    var golsContra = db.Gols.Count(g => g.JogadorId == jogador.Id && g.Contra);
                    jogador.SaldoGols = golsFavor - golsContra;
                }
            }
            db.SaveChanges();

            AtualizarClassificacao(null);
            return RedirectToAction("Index");
        }

        public ActionResult RegistrarGol(int id)
        {
            var partida = db.Partidas
                .Include(p => p.TimeCasa)
                .Include(p => p.TimeFora)
                .FirstOrDefault(p => p.Id == id);

            if (partida == null)
                return HttpNotFound();

            var jogadores = db.Jogadores
                .Where(j => j.TimeId == partida.TimeCasaId || j.TimeId == partida.TimeForaId)
                .ToList();

            ViewBag.Partida = partida;
            ViewBag.Jogadores = new SelectList(jogadores, "Id", "Nome");
            ViewBag.TipoGol = new SelectList(new[] { "Normal", "Penalti", "Falta" });
            return View(new Gol { PartidaId = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarGol(Gol gol)
        {
            if (ModelState.IsValid)
            {
                db.Gols.Add(gol);
                db.SaveChanges();

                var partidas = db.Partidas.Find(gol.PartidaId);
                var jogador = db.Jogadores.Find(gol.JogadorId);

                if (jogador.TimeId == partidas.TimeCasaId && !gol.Contra)
                    partidas.PlacarCasa = (partidas.PlacarCasa ?? 0) + 1;
                else if (jogador.TimeId == partidas.TimeForaId && !gol.Contra)
                    partidas.PlacarFora = (partidas.PlacarFora ?? 0) + 1;
                else if (gol.Contra && jogador.TimeId == partidas.TimeCasaId)
                    partidas.PlacarFora = (partidas.PlacarFora ?? 0) + 1;
                else if (gol.Contra && jogador.TimeId == partidas.TimeForaId)
                    partidas.PlacarCasa = (partidas.PlacarCasa ?? 0) + 1;

                // Atualizar o SaldoGols do jogador
                var golsFavor = db.Gols.Count(g => g.JogadorId == jogador.Id && !g.Contra);
                var golsContra = db.Gols.Count(g => g.JogadorId == jogador.Id && g.Contra);
                jogador.SaldoGols = golsFavor - golsContra;

                db.SaveChanges();
                AtualizarClassificacao(partidas);
                return RedirectToAction("Details", new { id = gol.PartidaId });
            }

            var partida = db.Partidas.Find(gol.PartidaId);
            var jogadores = db.Jogadores
                .Where(j => j.TimeId == partida.TimeCasaId || j.TimeId == partida.TimeForaId)
                .ToList();

            ViewBag.Partida = partida;
            ViewBag.Jogadores = new SelectList(jogadores, "Id", "Nome");
            ViewBag.TipoGol = new SelectList(new[] { "Normal", "Penalti", "Falta" });
            return View(gol);
        }

        private void AtualizarClassificacao(Partida partida)
        {
            var partidas = db.Partidas
                .Include(p => p.TimeCasa)
                .Include(p => p.TimeFora)
                .ToList();

            foreach (var time in db.Times.ToList())
            {
                var classificacao = db.Classificacoes
                    .SingleOrDefault(c => c.TimeId == time.Id);

                if (classificacao == null)
                {
                    classificacao = new Classificacao { TimeId = time.Id };
                    db.Classificacoes.Add(classificacao);
                }

                var jogosCasa = partidas.Where(p => p.TimeCasaId == time.Id).ToList();
                var jogosFora = partidas.Where(p => p.TimeForaId == time.Id).ToList();

                classificacao.Jogos = jogosCasa.Count + jogosFora.Count;
                classificacao.GolsPro = jogosCasa.Sum(p => p.PlacarCasa ?? 0) + jogosFora.Sum(p => p.PlacarFora ?? 0);
                classificacao.GolsContra = jogosCasa.Sum(p => p.PlacarFora ?? 0) + jogosFora.Sum(p => p.PlacarCasa ?? 0);
                classificacao.Vitorias = jogosCasa.Count(p => (p.PlacarCasa ?? 0) > (p.PlacarFora ?? 0)) +
                                        jogosFora.Count(p => (p.PlacarFora ?? 0) > (p.PlacarCasa ?? 0));
                classificacao.Empates = jogosCasa.Count(p => (p.PlacarCasa ?? 0) == (p.PlacarFora ?? 0)) +
                                        jogosFora.Count(p => (p.PlacarFora ?? 0) == (p.PlacarCasa ?? 0));
                classificacao.Derrotas = jogosCasa.Count(p => (p.PlacarCasa ?? 0) < (p.PlacarFora ?? 0)) +
                                         jogosFora.Count(p => (p.PlacarFora ?? 0) < (p.PlacarCasa ?? 0));
                classificacao.Pontos = (classificacao.Vitorias * 3) + classificacao.Empates;
            }

            db.SaveChanges();
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