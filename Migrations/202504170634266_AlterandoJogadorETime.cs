namespace LigaTabajara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterandoJogadorETime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Time", "CorUniformeString", c => c.String(nullable: false));
            DropColumn("dbo.Time", "CorPrimariaString");
            DropColumn("dbo.Time", "CorSecundariaString");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Time", "CorSecundariaString", c => c.String());
            AddColumn("dbo.Time", "CorPrimariaString", c => c.String());
            DropColumn("dbo.Time", "CorUniformeString");
        }
    }
}
