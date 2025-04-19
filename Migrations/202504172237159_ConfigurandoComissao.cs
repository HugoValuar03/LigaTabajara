namespace LigaTabajara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigurandoComissao : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ComissaoTecnica", new[] { "Time_Id" });
            RenameColumn(table: "dbo.ComissaoTecnica", name: "Time_Id", newName: "TimeId");
            AlterColumn("dbo.ComissaoTecnica", "TimeId", c => c.Int(nullable: false));
            CreateIndex("dbo.ComissaoTecnica", "TimeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ComissaoTecnica", new[] { "TimeId" });
            AlterColumn("dbo.ComissaoTecnica", "TimeId", c => c.Int());
            RenameColumn(table: "dbo.ComissaoTecnica", name: "TimeId", newName: "Time_Id");
            CreateIndex("dbo.ComissaoTecnica", "Time_Id");
        }
    }
}
