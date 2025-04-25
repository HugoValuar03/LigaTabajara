using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
	public enum CorUniforme
	{
		[Display(Name = "Primária")]
		PRIMARIA = 1,

		[Display(Name = "Secundária")]
		SECUNDARIA = 2
	}
}