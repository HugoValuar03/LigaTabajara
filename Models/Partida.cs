using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
    public class Partida
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Id do time mandante é obrigatório.")]
        public int TimeCasaId { get; set; }
        [ForeignKey("TimeCasaId")]
        public virtual Time TimeCasa { get; set; }

        [Required(ErrorMessage = "O Id do time visitante é obrigatório.")]
        public int TimeForaId { get; set; }
        [ForeignKey("TimeForaId")]
        public virtual Time TimeFora { get; set; }

        [Required(ErrorMessage = "A data e hora da partida são obrigatórias.")]
        [Display(Name = "Data e Hora")]
        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "O estádio da partida é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do estádio deve ter no máximo 100 caracteres.")]
        public string Estadio { get; set; }

        [Required(ErrorMessage = "O placar do time da casa é obrigatório.")]
        [Range(0, int.MaxValue, ErrorMessage = "O placar deve ser um número não negativo.")]
        public int PlacarCasa { get; set; }

        [Required(ErrorMessage = "O placar do time de fora é obrigatório.")]
        [Range(0, int.MaxValue, ErrorMessage = "O placar deve ser um número não negativo.")]
        public int PlacarFora { get; set; }

        public int Rodada { get; set; }

        public virtual ICollection<Gol> Gols { get; set; }

    }
}