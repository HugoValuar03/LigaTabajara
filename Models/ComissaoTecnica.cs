using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
	public class ComissaoTecnica
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string CargoString { get; set; }
		[NotMapped]
		public Cargo Cargo
		{
			get { return (Cargo)Enum.Parse(typeof(Cargo), this.CargoString); }
			set { this.CargoString = value.ToString(); }
		}
		public Time Time { get; set; }
	}
}