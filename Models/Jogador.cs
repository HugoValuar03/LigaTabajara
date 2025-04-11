using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
	public class Jogador
	{
        public int ID { get; set; }
        public string Nome { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        public string Nacionalidade { get; set; }
        public Posicao Posicao { get; set; }
        public int NumeroCamisa { get; set; }
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }
        public PePreferido PePreferido { get; set; }
        public int TimeId { get; set; }
        [ForeignKey("TimeId")]
        public virtual Time Time { get; set; }
    }
}