using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
    public class Time
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do time é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O estado do time é obrigatório.")]
        [StringLength(2, ErrorMessage = "O estado deve ter 2 caracteres.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O ano de fundação do time é obrigatório.")]
        [Display(Name = "Ano de Fundação")]
        [DataType(DataType.DateTime)]
        public DateTime AnoFundacao { get; set; }

        [Required(ErrorMessage = "O nome do estádio é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do estádio deve ter no máximo 100 caracteres.")]
        public string Estadio { get; set; }

        [Required(ErrorMessage = "A capacidade do estádio é obrigatória.")]
        [Display(Name = "Capacidade do Estádio")]
        [Range(0, int.MaxValue, ErrorMessage = "A capacidade do estádio deve ser um número positivo.")]
        public int CapacidadeEstadio { get; set; }

        [Required]
        public string CorUniformeString { get; set; }

        [NotMapped]
        public CorUniforme CorUniforme
        {
            get
            {
                return string.IsNullOrEmpty(CorUniformeString)
                    ? CorUniforme.PRIMARIA
                    : (CorUniforme)Enum.Parse(typeof(CorUniforme), CorUniformeString);
            }
            set
            {
                CorUniformeString = value.ToString();
            }
        }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [NotMapped]
        public string StatusFormatado
        {
            get { return Status == "APTO" ? "Apto" : "Não Apto"; }
            set { Status = value == "Apto" ? "APTO" : "NAO_APTO"; }
        }

        [JsonIgnore]
        public virtual ICollection<ComissaoTecnica> ComissaoTecnicas { get; set; }

        public virtual ICollection<Jogador> Jogadores { get; set; }

        public virtual ICollection<Partida> PartidasMandante { get; set; }
        public virtual ICollection<Partida> PartidasVisitante { get; set; }
    }
}