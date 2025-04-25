namespace LigaTabajara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSaldoGolsToJogador : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jogador", "SaldoGols", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jogador", "SaldoGols");
        }
    }
}
