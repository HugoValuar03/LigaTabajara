using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
    public class Time
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Estado { get; set; }
        public DateTime AnoFundacao { get; set; }
        public string Estadio { get; set; }
        public int CapacidadeEstadio { get; set; }
        public string CorString { get; set; }
        [NotMapped]
        public CorUniforme CorUniforme
        {
            get { return (CorUniforme)Enum.Parse(typeof(CorUniforme), this.CorString); }
            set { this.CorString = value.ToString(); }
        }
        public string Status { get; set; }
        public virtual ICollection<ComissaoTecnica> ComissaoTecnicas { get; set; }
    }
}