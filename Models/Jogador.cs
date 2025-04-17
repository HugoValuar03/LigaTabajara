using LigaTabajara.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Newtonsoft.Json;

namespace LigaTabajara.Models
{
    public class Jogador
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do jogador é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de nascimento do jogador é obrigatória.")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "A nacionalidade do jogador é obrigatória.")]
        [StringLength(50, ErrorMessage = "A nacionalidade deve ter no máximo 50 caracteres.")]
        public string Nacionalidade { get; set; }

        [Required(ErrorMessage = "A posição do jogador é obrigatória.")]
        public Posicao Posicao { get; set; }

        [Required(ErrorMessage = "O número da camisa do jogador é obrigatório.")]
        [Display(Name = "Número da Camisa")]
        [Range(1, 99, ErrorMessage = "O número da camisa deve estar entre 1 e 99.")]
        public int NumeroCamisa { get; set; }

        [Display(Name = "Altura")]
        public int Altura { get; set; }

        [Required(ErrorMessage = "O peso do jogador é obrigatório.")]
        [Display(Name = "Peso")]
        [Range(50.0, 150.0, ErrorMessage = "O peso deve estar entre 50,0 e 150,0 kg.")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "O pé preferido do jogador é obrigatório.")]
        [Display(Name = "Pé Preferido")]
        public PePreferido PePreferido { get; set; }

        [ForeignKey("Time")]
        public int TimeId { get; set; }

        [JsonIgnore] // Evita serialização circular
        public virtual Time Time { get; set; }
    }
}