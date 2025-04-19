    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace LigaTabajara.Models
    {
        public class Gol
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "O Id do jogador é obrigatório.")]
            [Display(Name = "Jogador")]
            public int JogadorId { get; set; }

            [ForeignKey("JogadorId")]
            public virtual Jogador Jogador { get; set; }

            [Required(ErrorMessage = "O Id da partida é obrigatório.")]
            [Display(Name = "Partida")]
            public int PartidaId { get; set; }

            [ForeignKey("PartidaId")]
            public virtual Partida Partida { get; set; }

            [Required(ErrorMessage = "O minuto do gol é obrigatório.")]
            [Display(Name = "Minuto do Gol")]
            [Range(1, 120, ErrorMessage = "O minuto do gol deve estar entre 1 e 120.")]
            public int Minuto { get; set; }  // Renomeado para Minuto (mais claro)

            [Display(Name = "Tipo do Gol")]
            [StringLength(50)]
            public string TipoGol { get; set; }  // Novo campo: "Normal", "Penalti", "Falta"

            [Display(Name = "Contra?")]
            public bool Contra { get; set; } = false;  // Novo campo: gol contra

        }
    }