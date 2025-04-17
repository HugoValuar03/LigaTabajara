using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
	public enum Status
	{
		[Display(Name = "Apto")]
		APTO = 1,
		[Display(Name = "Não Apto")]
		NAO_APTO = 2
	}
}