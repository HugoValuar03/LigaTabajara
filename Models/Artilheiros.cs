using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
	public class Artilheiros
	{
        public string NomeJogador { get; set; }
        public string NomeTime { get; set; } // Adicionado para corresponder à view
        public int TotalGols { get; set; }
    }
}