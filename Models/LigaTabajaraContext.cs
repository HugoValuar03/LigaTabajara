using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LigaTabajara.Models
{
    public class LigaTabajaraContext : DbContext
    {
        public LigaTabajaraContext() : base("name=LigaTabajaraContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        // DbSets para todas as entidades
        public virtual DbSet<Time> Times { get; set; }
        public virtual DbSet<ComissaoTecnica> ComissaoTecnicas { get; set; }
        public virtual DbSet<Jogador> Jogadores { get; set; }
        public virtual DbSet<Partida> Partidas { get; set; }
        public virtual DbSet<Gol> Gols { get; set; }
        public virtual DbSet<Classificacao> Classificacoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>(); //Remove a pluralização automática dos nomes das tabelas
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>(); //Impede o delete em cascata
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.ManyToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Jogador>()
                .HasRequired(j => j.Time)
                .WithMany(t => t.Jogadores)
                .HasForeignKey(j => j.TimeId)
                .WillCascadeOnDelete(false);
        }
    }
}