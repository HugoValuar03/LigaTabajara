using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
	public enum Posicao
	{
		[Display(Name = "Goleiro")]
		GOLEIRO = 1,
        [Display(Name = "Zagueiro")]
        ZAGUEIRO = 2,
        [Display(Name = "Volante")]
        VOLANTE = 3,
        [Display(Name = "Meia")]
        MEIA = 4,
        [Display(Name = "Atacante")]
        ATACANTE = 5,
        [Display(Name = "Meia-Atacante")]
        MEIA_ATACANTE = 6,
	}
}