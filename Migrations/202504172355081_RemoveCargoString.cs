namespace LigaTabajara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCargoString : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ComissaoTecnica", "CargoString");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ComissaoTecnica", "CargoString", c => c.String());
        }
    }
}
