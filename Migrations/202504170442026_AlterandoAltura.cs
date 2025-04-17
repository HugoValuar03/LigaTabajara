namespace LigaTabajara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterandoAltura : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jogador", "Altura", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jogador", "Altura", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
