using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaTabajara.Models
{
    public class Partida
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Id do time mandante é obrigatório.")]
        [Display(Name = "Time Mandante")]
        public int TimeCasaId { get; set; }

        [ForeignKey("TimeCasaId")]
        public virtual Time TimeCasa { get; set; }

        [Required(ErrorMessage = "O Id do time visitante é obrigatório.")]
        [Display(Name = "Time Visitante")]
        public int TimeForaId { get; set; }

        [ForeignKey("TimeForaId")]
        public virtual Time TimeFora { get; set; }

        [Required(ErrorMessage = "A data e hora da partida são obrigatórias.")]
        [Display(Name = "Data e Hora")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "O estádio da partida é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do estádio deve ter no máximo 100 caracteres.")]
        public string Estadio { get; set; }

        [Display(Name = "Placar Casa")]
        [Range(0, 50, ErrorMessage = "O placar deve ser entre 0 e 50.")]
        public int? PlacarCasa { get; set; }  // Alterado para nullable (antes do jogo)

        [Display(Name = "Placar Visitante")]
        [Range(0, 50, ErrorMessage = "O placar deve ser entre 0 e 50.")]
        public int? PlacarFora { get; set; }  // Alterado para nullable (antes do jogo)

        [Required(ErrorMessage = "A rodada é obrigatória.")]
        [Range(1, 38, ErrorMessage = "A rodada deve ser entre 1 e 38.")]
        public int Rodada { get; set; }

        public virtual ICollection<Gol> Gols { get; set; } = new List<Gol>();
    }
}