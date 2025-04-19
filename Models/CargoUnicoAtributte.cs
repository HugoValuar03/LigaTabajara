using LigaTabajara.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
	public class CargoUnicoAtributte : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("O cargo é obrigatório.");

            var cargoSelecionado = (Cargo)value;
            var comissaoAtual = (ComissaoTecnica)validationContext.ObjectInstance;

            // Cria uma nova instância manualmente do contexto
            using (var dbContext = new LigaTabajaraContext())
            {
                bool cargoJaExiste = dbContext.ComissaoTecnicas
                    .Any(ct => ct.TimeId == comissaoAtual.TimeId &&
                              ct.Cargo == cargoSelecionado &&
                              ct.Id != comissaoAtual.Id);

                if (cargoJaExiste)
                {
                    return new ValidationResult($"Já existe um {cargoSelecionado} neste time.");
                }
            }

            return ValidationResult.Success;
        }
    }
}