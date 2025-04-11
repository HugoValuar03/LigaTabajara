namespace LigaTabajara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandoTabelaJogador : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComissaoTecnicas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        CargoString = c.String(),
                        Time_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Times", t => t.Time_Id)
                .Index(t => t.Time_Id);
            
            CreateTable(
                "dbo.Times",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Estado = c.String(),
                        AnoFundacao = c.DateTime(nullable: false),
                        Estadio = c.String(),
                        CapacidadeEstadio = c.Int(nullable: false),
                        CorString = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Jogadors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Jogadors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        DataNascimento = c.DateTime(nullable: false),
                        Nacionalidade = c.String(),
                        Posicao = c.Int(nullable: false),
                        NumeroCamisa = c.Int(nullable: false),
                        Altura = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Peso = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PePreferido = c.Int(nullable: false),
                        Time = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.ComissaoTecnicas", "Time_Id", "dbo.Times");
            DropIndex("dbo.ComissaoTecnicas", new[] { "Time_Id" });
            DropTable("dbo.Times");
            DropTable("dbo.ComissaoTecnicas");
        }
    }
}
