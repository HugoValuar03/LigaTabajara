namespace LigaTabajara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandoJogador : DbMigration
    {
        public override void Up()
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
                        TimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Times", t => t.TimeId, cascadeDelete: true)
                .Index(t => t.TimeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jogadors", "TimeId", "dbo.Times");
            DropIndex("dbo.Jogadors", new[] { "TimeId" });
            DropTable("dbo.Jogadors");
        }
    }
}
