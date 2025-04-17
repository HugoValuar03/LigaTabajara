using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
	public class Gol
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "O Id do jogador é obrigatório.")]
        public int JogadorId { get; set; }

        [ForeignKey("JogadorId")]
        public virtual Jogador Jogador { get; set; }
        public int PartidaId { get; set; }

        [ForeignKey("PartidaId")]
        public virtual Partida Partida { get; set; }

        [Required(ErrorMessage = "O minuto do gol é obrigatório.")]
        [Range(0, 120, ErrorMessage = "O minuto do gol deve estar entre 0 e 120.")]
        public int MinutoGol { get; set; }
    }
}