using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
    public enum Cargo
    {
        [Display(Name = "Treinador")]
        TREINADOR = 1,
        [Display(Name = "Auxiliar")]
        AUXILIAR = 2,
        [Display(Name ="Preparador Fisico")]
        PREPARADOR_FISICO = 3,
        [Display(Name = "Fisiologista")]
        FISIOLOGISTA = 4,
        [Display(Name = "Treinador do Goleiro")]
        TREINADOR_DE_GOLEIRO = 5,
        [Display(Name = "Fisioterapeuta")]
        FISIOTERAPEUTA = 6
    }
}