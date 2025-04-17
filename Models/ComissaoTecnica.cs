﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
	public class ComissaoTecnica
	{
		public int Id { get; set; }

        [Required(ErrorMessage = "O nome do membro da comissão técnica é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }
        public string CargoString { get; set; }

        [Required(ErrorMessage = "A data de nascimento do membro da comissão técnica é obrigatória.")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
		[NotMapped]
		public Cargo Cargo
		{
			get { return (Cargo)Enum.Parse(typeof(Cargo), this.CargoString); }
			set { this.CargoString = value.ToString(); }
		}
		public Time Time { get; set; }
	}
}