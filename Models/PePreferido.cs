﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
	public enum PePreferido
	{
		[Display(Name = "Esquerdo")]
		ESQUERDO = 1,
		[Display(Name = "Direito")]
		DIREITO = 2,
		[Display(Name = "Ambidestro")]
		AMBIDESTRO = 3
	}
}