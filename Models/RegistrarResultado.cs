using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
	public class RegistrarResultado
	{
        [Required]
        public int PartidaId { get; set; }

        [Display(Name = "Placar Time Mandante")]
        [Range(0, 50, ErrorMessage = "O placar deve ser entre 0 e 50.")]
        public int PlacarCasa { get; set; }

        [Display(Name = "Placar Time Visitante")]
        [Range(0, 50, ErrorMessage = "O placar deve ser entre 0 e 50.")]
        public int PlacarFora { get; set; }

        [Display(Name = "Descrição do Resultado")]
        public string DescricaoPartida
        {
            get
            {
                return $"Resultado: {PlacarCasa} x {PlacarFora}";
            }
        }
    }
}