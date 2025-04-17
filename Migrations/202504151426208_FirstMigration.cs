namespace LigaTabajara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComissaoTecnicas", "Time_Id", "dbo.Times");
            DropIndex("dbo.ComissaoTecnicas", new[] { "Time_Id" });
            DropTable("dbo.Times");
            DropTable("dbo.ComissaoTecnicas");
        }
    }
}
