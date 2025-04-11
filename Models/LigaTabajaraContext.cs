using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
    public class LigaTabajaraContext : DbContext
    {
        public LigaTabajaraContext() : base("LigaTabajaraContext")
        {
            // Configurações adicionais do contexto
        }

        // DbSets para todas as entidades
        public DbSet<Time> Times { get; set; }
        public DbSet<ComissaoTecnica> ComissaoTecnicas { get; set; }
    }
}