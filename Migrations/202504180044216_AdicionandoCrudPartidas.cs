namespace LigaTabajara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandoCrudPartidas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classificacao", "Jogos", c => c.Int(nullable: false));
            AddColumn("dbo.Classificacao", "UltimosJogos", c => c.String(maxLength: 10));
            AddColumn("dbo.Gol", "Minuto", c => c.Int(nullable: false));
            AddColumn("dbo.Gol", "TipoGol", c => c.String(maxLength: 50));
            AddColumn("dbo.Gol", "Contra", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Partida", "PlacarCasa", c => c.Int());
            AlterColumn("dbo.Partida", "PlacarFora", c => c.Int());
            DropColumn("dbo.Classificacao", "SaldoGols");
            DropColumn("dbo.Gol", "MinutoGol");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Gol", "MinutoGol", c => c.Int(nullable: false));
            AddColumn("dbo.Classificacao", "SaldoGols", c => c.Int(nullable: false));
            AlterColumn("dbo.Partida", "PlacarFora", c => c.Int(nullable: false));
            AlterColumn("dbo.Partida", "PlacarCasa", c => c.Int(nullable: false));
            DropColumn("dbo.Gol", "Contra");
            DropColumn("dbo.Gol", "TipoGol");
            DropColumn("dbo.Gol", "Minuto");
            DropColumn("dbo.Classificacao", "UltimosJogos");
            DropColumn("dbo.Classificacao", "Jogos");
        }
    }
}
