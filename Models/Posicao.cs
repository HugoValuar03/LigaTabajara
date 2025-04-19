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

        [Display(Name = "Zagueiro Central")]
        ZAGUEIRO_CENTRAL = 3,

        [Display(Name = "Lateral Direito")]
        LATERAL_DIREITO = 4,

        [Display(Name = "Lateral Esquerdo")]
        LATERAL_ESQUERDO = 5,

        [Display(Name = "Volante")]
        VOLANTE = 6,

        [Display(Name = "Primeiro Volante")]
        PRIMEIRO_VOLANTE = 7,

        [Display(Name = "Segundo Volante")]
        SEGUNDO_VOLANTE = 8,

        [Display(Name = "Meia Direita")]
        MEIA_DIREITA = 9,

        [Display(Name = "Meia Esquerda")]
        MEIA_ESQUERDA = 10,

        [Display(Name = "Meia Central")]
        MEIA_CENTRAL = 11,

        [Display(Name = "Meia Atacante")]
        MEIA_ATACANTE = 12,

        [Display(Name = "Ponta Direita")]
        PONTA_DIREITA = 13,

        [Display(Name = "Ponta Esquerda")]
        PONTA_ESQUERDA = 14,

        [Display(Name = "Atacante")]
        ATACANTE = 15,

        [Display(Name = "Centroavante")]
        CENTROAVANTE = 16,

        [Display(Name = "Segundo Atacante")]
        SEGUNDO_ATACANTE = 17
    }
}