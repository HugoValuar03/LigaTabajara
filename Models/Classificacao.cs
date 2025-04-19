using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaTabajara.Models
{
    public class Classificacao
    {
        public int Id { get; set; }

        [Required]
        public int TimeId { get; set; }

        public int Pontos { get; set; }
        public int Jogos { get; set; }
        public int Vitorias { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }
        public int GolsPro { get; set; }
        public int GolsContra { get; set; }
        public int SaldoGols { get; set; }

        public virtual Time Time { get; set; }

        internal static void Add(Classificacao classificacao)
        {
            throw new NotImplementedException();
        }
    }
}