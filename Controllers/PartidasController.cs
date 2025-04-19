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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var partida = db.Partidas
                .Include(p => p.TimeCasa)
                .Include(p => p.TimeFora)
                .FirstOrDefault(p => p.Id == id);

            if (partida == null)
                return HttpNotFound();

            var gols = db.Gols
                .Include(g => g.Jogador)
                .Include(g => g.Jogador.Time)
                .Where(g => g.PartidaId == id)
                .ToList();

            var golsCasa = gols
                .Where(g => (g.Jogador.TimeId == partida.TimeCasaId && !g.Contra) || (g.Jogador.TimeId == partida.TimeForaId && g.Contra))
                .ToList();
            var golsFora = gols
                .Where(g => (g.Jogador.TimeId == partida.TimeForaId && !g.Contra) || (g.Jogador.TimeId == partida.TimeCasaId && g.Contra))
                .ToList();

            var artilheiros = gols
                .GroupBy(g => g.Jogador)
                .Select(g => new Artilheiros
                {
                    NomeJogador = g.Key.Nome,
                    NomeTime = g.Key.Time.Nome,
                    TotalGols = g.Count(x => !x.Contra)
                })
                .Where(a => a.TotalGols > 0)
                .OrderByDescending(a => a.TotalGols)
                .ToList();

            ViewBag.GolsCasa = golsCasa;
            ViewBag.GolsFora = golsFora;
            ViewBag.Artilheiros = artilheiros;

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

            var partida = db.Partidas.Find(id);
            if (partida == null)
                return HttpNotFound();

            return View(partida);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var partida = db.Partidas.Find(id);
            db.Gols.RemoveRange(db.Gols.Where(g => g.PartidaId == id));
            db.Partidas.Remove(partida);
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

        private void AtualizarClassificacao(Partida partidaAtualizada)
        {
            var times = db.Times.ToList();
            foreach (var time in times)
            {
                var tabela = db.Classificacoes.FirstOrDefault(t => t.TimeId == time.Id);
                if (tabela == null)
                {
                    tabela = new Classificacao { TimeId = time.Id };
                    db.Classificacoes.Add(tabela);
                }

                tabela.Pontos = 0;
                tabela.Jogos = 0;
                tabela.Vitorias = 0;
                tabela.Empates = 0;
                tabela.Derrotas = 0;
                tabela.GolsPro = 0;
                tabela.GolsContra = 0;
                tabela.SaldoGols = 0;

                var partidas = db.Partidas
                    .Where(p => (p.TimeCasaId == time.Id || p.TimeForaId == time.Id) && p.PlacarCasa.HasValue && p.PlacarFora.HasValue)
                    .ToList();

                foreach (var partida in partidas)
                {
                    tabela.Jogos++;
                    if (partida.TimeCasaId == time.Id)
                    {
                        tabela.GolsPro += partida.PlacarCasa.Value;
                        tabela.GolsContra += partida.PlacarFora.Value;
                        if (partida.PlacarCasa > partida.PlacarFora)
                        {
                            tabela.Vitorias++;
                            tabela.Pontos += 3;
                        }
                        else if (partida.PlacarCasa == partida.PlacarFora)
                        {
                            tabela.Empates++;
                            tabela.Pontos += 1;
                        }
                        else
                        {
                            tabela.Derrotas++;
                        }
                    }
                    else
                    {
                        tabela.GolsPro += partida.PlacarFora.Value;
                        tabela.GolsContra += partida.PlacarCasa.Value;
                        if (partida.PlacarFora > partida.PlacarCasa)
                        {
                            tabela.Vitorias++;
                            tabela.Pontos += 3;
                        }
                        else if (partida.PlacarFora == partida.PlacarCasa)
                        {
                            tabela.Empates++;
                            tabela.Pontos += 1;
                        }
                        else
                        {
                            tabela.Derrotas++;
                        }
                    }
                    tabela.SaldoGols = tabela.GolsPro - tabela.GolsContra;
                }
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