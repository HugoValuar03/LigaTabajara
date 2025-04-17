namespace LigaTabajara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompletandoModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classificacaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeId = c.Int(nullable: false),
                        Pontos = c.Int(nullable: false),
                        Vitorias = c.Int(nullable: false),
                        Empates = c.Int(nullable: false),
                        Derrotas = c.Int(nullable: false),
                        GolsPro = c.Int(nullable: false),
                        GolsContra = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Times", t => t.TimeId, cascadeDelete: true)
                .Index(t => t.TimeId);
            
            CreateTable(
                "dbo.Gols",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JogadorId = c.Int(nullable: false),
                        PartidaId = c.Int(nullable: false),
                        MinutoGol = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jogadors", t => t.JogadorId, cascadeDelete: true)
                .ForeignKey("dbo.Partidas", t => t.PartidaId, cascadeDelete: true)
                .Index(t => t.JogadorId)
                .Index(t => t.PartidaId);
            
            CreateTable(
                "dbo.Partidas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeCasaId = c.Int(nullable: false),
                        TimeForaId = c.Int(nullable: false),
                        DataHora = c.DateTime(nullable: false),
                        Estadio = c.String(),
                        PlacarCasa = c.Int(nullable: false),
                        PlacarFora = c.Int(nullable: false),
                        Rodada = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Times", t => t.TimeCasaId, cascadeDelete: false)
                .ForeignKey("dbo.Times", t => t.TimeForaId, cascadeDelete: false)
                .Index(t => t.TimeCasaId)
                .Index(t => t.TimeForaId);
            
            AddColumn("dbo.ComissaoTecnicas", "DataNascimento", c => c.DateTime(nullable: false));
            AddColumn("dbo.Times", "CorPrimariaString", c => c.String());
            AddColumn("dbo.Times", "CorSecundariaString", c => c.String());
            DropColumn("dbo.Times", "CorString");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Times", "CorString", c => c.String());
            DropForeignKey("dbo.Partidas", "TimeForaId", "dbo.Times");
            DropForeignKey("dbo.Partidas", "TimeCasaId", "dbo.Times");
            DropForeignKey("dbo.Gols", "PartidaId", "dbo.Partidas");
            DropForeignKey("dbo.Gols", "JogadorId", "dbo.Jogadors");
            DropForeignKey("dbo.Classificacaos", "TimeId", "dbo.Times");
            DropIndex("dbo.Partidas", new[] { "TimeForaId" });
            DropIndex("dbo.Partidas", new[] { "TimeCasaId" });
            DropIndex("dbo.Gols", new[] { "PartidaId" });
            DropIndex("dbo.Gols", new[] { "JogadorId" });
            DropIndex("dbo.Classificacaos", new[] { "TimeId" });
            DropColumn("dbo.Times", "CorSecundariaString");
            DropColumn("dbo.Times", "CorPrimariaString");
            DropColumn("dbo.ComissaoTecnicas", "DataNascimento");
            DropTable("dbo.Partidas");
            DropTable("dbo.Gols");
            DropTable("dbo.Classificacaos");
        }
    }
}
