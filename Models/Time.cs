namespace LigaTabajara.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Time
    {
        public Time()
        {
            Jogadores = new HashSet<Jogador>();
            ComissaoTecnicas = new HashSet<ComissaoTecnica>();
            PartidasCasa = new HashSet<Partida>();
            PartidasFora = new HashSet<Partida>();
        }

        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        [Display(Name = "Ano de Fundação")]
        public DateTime AnoFundacao { get; set; }

        [Required]
        public string Estadio { get; set; }

        [Required]
        [Display(Name = "Capacidade do Estádio")]
        public int CapacidadeEstadio { get; set; }

        [Required]
        [Display(Name = "Cor do Uniforme")]
        public CorUniforme CorUniforme { get; set; }

        [Required]
        public Status Status { get; set; }

        [NotMapped]
        public bool IsApto
        {
            get
            {
                int numeroJogadores = Jogadores?.Count ?? 0;
                int numeroComissaoTecnica = ComissaoTecnicas?.Count ?? 0;
                return numeroJogadores >= 30 && numeroComissaoTecnica >= 5;
            }
        }

        // Método para atualizar o Status com base na aptidão
        public void AtualizarStatus()
        {
            Status = IsApto ? Status.APTO : Status.NAO_APTO;
        }

        public virtual ICollection<Jogador> Jogadores { get; set; }
        public virtual ICollection<ComissaoTecnica> ComissaoTecnicas { get; set; }
        public virtual ICollection<Partida> PartidasCasa { get; set; }
        public virtual ICollection<Partida> PartidasFora { get; set; }
    }
}