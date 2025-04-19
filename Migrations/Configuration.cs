namespace LigaTabajara.Migrations
{
    using LigaTabajara.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LigaTabajara.Models.LigaTabajaraContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LigaTabajara.Models.LigaTabajaraContext context)
        {
            // Seed para Times
            if (!context.Times.Any())
            {
                var times = new List<Time>
                {
                    new Time { Nome = "Flamengo", Estado = "RJ", AnoFundacao = new DateTime(1895, 11, 15), Estadio = "Maracanã", CapacidadeEstadio = 78838, CorUniforme = (CorUniforme)0, Status = (Status)0 },
                    new Time { Nome = "Palmeiras", Estado = "SP", AnoFundacao = new DateTime(1914, 8, 26), Estadio = "Allianz Parque", CapacidadeEstadio = 43713, CorUniforme = (CorUniforme)1, Status = (Status)0 },
                    new Time { Nome = "Corinthians", Estado = "SP", AnoFundacao = new DateTime(1910, 9, 1), Estadio = "Neo Química Arena", CapacidadeEstadio = 49205, CorUniforme = (CorUniforme)0, Status = (Status)0 },
                    new Time { Nome = "São Paulo", Estado = "SP", AnoFundacao = new DateTime(1930, 1, 25), Estadio = "Morumbi", CapacidadeEstadio = 66795, CorUniforme = (CorUniforme)1, Status = (Status)0 },
                    new Time { Nome = "Santos", Estado = "SP", AnoFundacao = new DateTime(1912, 4, 14), Estadio = "Vila Belmiro", CapacidadeEstadio = 16068, CorUniforme = (CorUniforme)0, Status = (Status)0 },
                    new Time { Nome = "Grêmio", Estado = "RS", AnoFundacao = new DateTime(1903, 9, 15), Estadio = "Arena do Grêmio", CapacidadeEstadio = 60540, CorUniforme = (CorUniforme)1, Status = (Status)0 },
                    new Time { Nome = "Internacional", Estado = "RS", AnoFundacao = new DateTime(1909, 4, 4), Estadio = "Beira-Rio", CapacidadeEstadio = 50128, CorUniforme = (CorUniforme)0, Status = (Status)0 },
                    new Time { Nome = "Atlético Mineiro", Estado = "MG", AnoFundacao = new DateTime(1908, 3, 25), Estadio = "Mineirão", CapacidadeEstadio = 61846, CorUniforme = (CorUniforme)1, Status = (Status)0 },
                    new Time { Nome = "Cruzeiro", Estado = "MG", AnoFundacao = new DateTime(1921, 1, 2), Estadio = "Mineirão", CapacidadeEstadio = 61846, CorUniforme = (CorUniforme)0, Status = (Status)0 },
                    new Time { Nome = "Fluminense", Estado = "RJ", AnoFundacao = new DateTime(1902, 7, 21), Estadio = "Maracanã", CapacidadeEstadio = 78838, CorUniforme = (CorUniforme)1, Status = (Status)0 },
                    new Time { Nome = "Vasco da Gama", Estado = "RJ", AnoFundacao = new DateTime(1898, 8, 21), Estadio = "São Januário", CapacidadeEstadio = 21880, CorUniforme = (CorUniforme)0, Status = (Status)0 },
                    new Time { Nome = "Botafogo", Estado = "RJ", AnoFundacao = new DateTime(1904, 7, 1), Estadio = "Nilton Santos", CapacidadeEstadio = 46931, CorUniforme = (CorUniforme)1, Status = (Status)0 },
                    new Time { Nome = "Athletico Paranaense", Estado = "PR", AnoFundacao = new DateTime(1924, 3, 26), Estadio = "Arena da Baixada", CapacidadeEstadio = 42372, CorUniforme = (CorUniforme)0, Status = (Status)0 },
                    new Time { Nome = "Coritiba", Estado = "PR", AnoFundacao = new DateTime(1909, 10, 12), Estadio = "Couto Pereira", CapacidadeEstadio = 40502, CorUniforme = (CorUniforme)1, Status = (Status)0 },
                    new Time { Nome = "Bahia", Estado = "BA", AnoFundacao = new DateTime(1931, 1, 1), Estadio = "Arena Fonte Nova", CapacidadeEstadio = 47947, CorUniforme = (CorUniforme)0, Status = (Status)0 },
                    new Time { Nome = "Sport Recife", Estado = "PE", AnoFundacao = new DateTime(1905, 5, 13), Estadio = "Ilha do Retiro", CapacidadeEstadio = 32983, CorUniforme = (CorUniforme)1, Status = (Status)0 },
                    new Time { Nome = "Ceará", Estado = "CE", AnoFundacao = new DateTime(1914, 6, 2), Estadio = "Castelão", CapacidadeEstadio = 63903, CorUniforme = (CorUniforme)0, Status = (Status)0 },
                    new Time { Nome = "Fortaleza", Estado = "CE", AnoFundacao = new DateTime(1918, 10, 18), Estadio = "Castelão", CapacidadeEstadio = 63903, CorUniforme = (CorUniforme)1, Status = (Status)0 },
                    new Time { Nome = "Goiás", Estado = "GO", AnoFundacao = new DateTime(1943, 4, 6), Estadio = "Serra Dourada", CapacidadeEstadio = 50049, CorUniforme = (CorUniforme)0, Status = (Status)0 },
                    new Time { Nome = "Red Bull Bragantino", Estado = "SP", AnoFundacao = new DateTime(1928, 1, 8), Estadio = "Nabi Abi Chedid", CapacidadeEstadio = 17128, CorUniforme = (CorUniforme)1, Status = (Status)0 }
                };

                context.Times.AddRange(times);
                context.SaveChanges();
            }

            // Seed para Jogadores
            if (!context.Jogadores.Any())
            {
                var times = context.Times.ToList();
                var random = new Random();
                string[] nomes = { "João", "Pedro", "Lucas", "Gabriel", "Matheus", "Rafael", "Thiago", "Bruno" };
                string[] sobrenomes = { "Silva", "Santos", "Oliveira", "Souza", "Rodrigues", "Costa", "Almeida", "Pereira" };
                string[] nacionalidades = { "Brasil", "Argentina", "Uruguai", "Paraguai" };

                foreach (var time in times)
                {
                    var jogadores = new List<Jogador>();
                    int numeroJogadores = time.Nome == "Red Bull Bragantino" ? 1 : 30; // Simulando o problema: Red Bull Bragantino terá apenas 1 jogador

                    for (int i = 1; i <= numeroJogadores; i++)
                    {
                        var posicao = i <= 3 ? (Posicao)0 :
                                     i <= 11 ? (Posicao)random.Next(1, 4) :
                                     i <= 21 ? (Posicao)random.Next(4, 8) :
                                     (Posicao)random.Next(8, 11);

                        jogadores.Add(new Jogador
                        {
                            Nome = $"{nomes[random.Next(nomes.Length)]} {sobrenomes[random.Next(sobrenomes.Length)]}",
                            DataNascimento = new DateTime(random.Next(1985, 2003), random.Next(1, 13), random.Next(1, 28)),
                            Nacionalidade = nacionalidades[random.Next(nacionalidades.Length)],
                            Posicao = posicao,
                            NumeroCamisa = i,
                            Altura = random.Next(165, 200),
                            Peso = (decimal)Math.Round(55 + random.NextDouble() * 45, 1),
                            PePreferido = (PePreferido)random.Next(0, 3),
                            TimeId = time.Id
                        });
                    }
                    context.Jogadores.AddRange(jogadores);
                }
                context.SaveChanges();
            }

            // Seed para ComissaoTecnicas
            if (!context.ComissaoTecnicas.Any())
            {
                var times = context.Times.ToList();
                var random = new Random();
                var nomes = new[] { "Carlos", "Eduardo", "Roberto", "Ricardo", "Fernando", "Bruno", "Paulo", "Jorge" };
                var sobrenomes = new[] { "Almeida", "Costa", "Ferreira", "Lima", "Mendes", "Pereira", "Rocha", "Vieira" };

                foreach (var time in times)
                {
                    var membros = new List<ComissaoTecnica>();
                    int numeroMembros = time.Nome == "Red Bull Bragantino" ? 1 : 5; // Simulando o problema: Red Bull Bragantino terá apenas 1 membro

                    for (int i = 0; i < numeroMembros; i++)
                    {
                        membros.Add(new ComissaoTecnica
                        {
                            Nome = $"{nomes[random.Next(nomes.Length)]} {sobrenomes[random.Next(sobrenomes.Length)]}",
                            Cargo = (Cargo)i,
                            DataNascimento = new DateTime(random.Next(1960, 1990), random.Next(1, 13), random.Next(1, 28)),
                            TimeId = time.Id
                        });
                    }
                    context.ComissaoTecnicas.AddRange(membros);
                }
                context.SaveChanges();
            }

            var todosTimes = context.Times.ToList();
            foreach (var time in todosTimes)
            {
                int numeroJogadores = context.Jogadores.Count(j => j.TimeId == time.Id);
                int numeroComissaoTecnica = context.ComissaoTecnicas.Count(ct => ct.TimeId == time.Id);

                time.Status = (numeroJogadores >= 30 && numeroComissaoTecnica >= 5) ? Status.APTO : Status.NAO_APTO;
            }
            context.SaveChanges();

            // Seed para Partidas
            if (!context.Partidas.Any())
            {
                var times = context.Times.ToList();
                var jogadores = context.Jogadores.ToList();
                var random = new Random();
                int rodada = 1;

                // Jogos de ida
                for (int i = 0; i < times.Count; i++)
                {
                    for (int j = i + 1; j < times.Count; j++)
                    {
                        var timeCasa = times[i];
                        var timeFora = times[j];

                        int placarCasa = random.Next(0, 5);
                        int placarFora = random.Next(0, 5);

                        var partida = new Partida
                        {
                            TimeCasaId = timeCasa.Id,
                            TimeForaId = timeFora.Id,
                            DataHora = DateTime.Now.AddDays(rodada * 7),
                            Estadio = timeCasa.Estadio,
                            Rodada = rodada,
                            PlacarCasa = placarCasa,
                            PlacarFora = placarFora
                        };

                        context.Partidas.Add(partida);
                        context.SaveChanges();

                        var jogadoresTimeCasa = jogadores.Where(jogador => jogador.TimeId == timeCasa.Id).ToList();
                        var jogadoresTimeFora = jogadores.Where(jogador => jogador.TimeId == timeFora.Id).ToList();
                        DistribuirGols(context, partida, jogadoresTimeCasa, jogadoresTimeFora, placarCasa, true, random);
                        DistribuirGols(context, partida, jogadoresTimeFora, jogadoresTimeCasa, placarFora, false, random);

                        if (context.Partidas.Count() % 10 == 0)
                            rodada++;
                    }
                }

                // Jogos de volta
                for (int i = 0; i < times.Count; i++)
                {
                    for (int j = i + 1; j < times.Count; j++)
                    {
                        var timeCasa = times[j];
                        var timeFora = times[i];

                        int placarCasa = random.Next(0, 5);
                        int placarFora = random.Next(0, 5);

                        var partida = new Partida
                        {
                            TimeCasaId = timeCasa.Id,
                            TimeForaId = timeFora.Id,
                            DataHora = DateTime.Now.AddDays(rodada * 7),
                            Estadio = timeCasa.Estadio,
                            Rodada = rodada,
                            PlacarCasa = placarCasa,
                            PlacarFora = placarFora
                        };

                        context.Partidas.Add(partida);
                        context.SaveChanges();

                        var jogadoresTimeCasa = jogadores.Where(jogador => jogador.TimeId == timeCasa.Id).ToList();
                        var jogadoresTimeFora = jogadores.Where(jogador => jogador.TimeId == timeFora.Id).ToList();
                        DistribuirGols(context, partida, jogadoresTimeCasa, jogadoresTimeFora, placarCasa, true, random);
                        DistribuirGols(context, partida, jogadoresTimeFora, jogadoresTimeCasa, placarFora, false, random);

                        if (context.Partidas.Count() % 10 == 0)
                            rodada++;
                    }
                }
                context.SaveChanges();
            }

            // Seed para Tabela
            if (!context.Classificacoes.Any())
            {
                var times = context.Times.ToList();
                var partidas = context.Partidas.ToList();

                foreach (var time in times)
                {
                    var classificacao = new Classificacao
                    {
                        TimeId = time.Id,
                        Pontos = 0,
                        Jogos = 0,
                        Vitorias = 0,
                        Empates = 0,
                        Derrotas = 0,
                        GolsPro = 0,
                        GolsContra = 0,
                        SaldoGols = 0
                    };

                    var partidasTime = partidas.Where(p => p.TimeCasaId == time.Id || p.TimeForaId == time.Id).ToList();
                    foreach (var partida in partidasTime)
                    {
                        if (partida.PlacarCasa.HasValue && partida.PlacarFora.HasValue)
                        {
                            classificacao.Jogos++;
                            if (partida.TimeCasaId == time.Id)
                            {
                                classificacao.GolsPro += partida.PlacarCasa.Value;
                                classificacao.GolsContra += partida.PlacarFora.Value;
                                if (partida.PlacarCasa > partida.PlacarFora)
                                {
                                    classificacao.Vitorias++;
                                    classificacao.Pontos += 3;
                                }
                                else if (partida.PlacarCasa == partida.PlacarFora)
                                {
                                    classificacao.Empates++;
                                    classificacao.Pontos += 1;
                                }
                                else
                                {
                                    classificacao.Derrotas++;
                                }
                            }
                            else
                            {
                                classificacao.GolsPro += partida.PlacarFora.Value;
                                classificacao.GolsContra += partida.PlacarCasa.Value;
                                if (partida.PlacarFora > partida.PlacarCasa)
                                {
                                    classificacao.Vitorias++;
                                    classificacao.Pontos += 3;
                                }
                                else if (partida.PlacarFora == partida.PlacarCasa)
                                {
                                    classificacao.Empates++;
                                    classificacao.Pontos += 1;
                                }
                                else
                                {
                                    classificacao.Derrotas++;
                                }
                            }
                            classificacao.SaldoGols = classificacao.GolsPro - classificacao.GolsContra;
                        }
                    }context.Classificacoes.Add(classificacao);
                }
                context.SaveChanges();
            }
        }

        private void DistribuirGols(LigaTabajaraContext context, Partida partida, List<Jogador> jogadoresTime, List<Jogador> jogadoresTimeAdversario, int totalGols, bool isTimeCasa, Random random)
        {
            if (totalGols == 0) return;

            var gols = new List<Gol>();
            var tiposGol = new[] { "Normal", "Penalti", "Falta" };
            double probabilidadeGolContra = 0.1;

            for (int i = 0; i < totalGols; i++)
            {
                bool isGolContra = random.NextDouble() < probabilidadeGolContra;
                Jogador jogadorSelecionado;

                if (isGolContra)
                {
                    jogadorSelecionado = jogadoresTimeAdversario[random.Next(jogadoresTimeAdversario.Count)];
                }
                else
                {
                    var jogadoresDisponiveis = jogadoresTime.Where(jogador => jogador.Posicao != (Posicao)0).ToList();
                    if (!jogadoresDisponiveis.Any()) jogadoresDisponiveis = jogadoresTime;
                    jogadorSelecionado = jogadoresDisponiveis[random.Next(jogadoresDisponiveis.Count)];
                }

                gols.Add(new Gol
                {
                    PartidaId = partida.Id,
                    JogadorId = jogadorSelecionado.Id,
                    Minuto = random.Next(1, 90),
                    TipoGol = tiposGol[random.Next(tiposGol.Length)],
                    Contra = isGolContra
                });
            }

            context.Gols.AddRange(gols);
        }
    }
}