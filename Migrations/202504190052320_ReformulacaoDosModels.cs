namespace LigaTabajara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReformulacaoDosModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classificacao", "SaldoGols", c => c.Int(nullable: false));
            AddColumn("dbo.Time", "CorUniforme", c => c.Int(nullable: false));
            AlterColumn("dbo.Time", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Time", "Estado", c => c.String(nullable: false));
            AlterColumn("dbo.Time", "Estadio", c => c.String(nullable: false));

            AddColumn("dbo.Time", "StatusTemp", c => c.Int(nullable: false, defaultValue: 0));

            Sql("UPDATE [dbo].[Time] SET [StatusTemp] = 0 WHERE [Status] = 'APTO'");
            Sql("UPDATE [dbo].[Time] SET [StatusTemp] = 1 WHERE [Status] = 'NAO_APTO'");

            DropColumn("dbo.Time", "Status");

            RenameColumn("dbo.Time", "StatusTemp", "Status");

            DropColumn("dbo.Classificacao", "UltimosJogos");
            DropColumn("dbo.Time", "CorUniformeString");
        }
        
        public override void Down()
        {
            Sql("UPDATE [dbo].[Time] SET [Status] = 'APTO' WHERE [Status] = 0");
            Sql("UPDATE [dbo].[Time] SET [Status] = 'NAO_APTO' WHERE [Status] = 1");

            AddColumn("dbo.Time", "CorUniformeString", c => c.String(nullable: false));
            AddColumn("dbo.Classificacao", "UltimosJogos", c => c.String(maxLength: 10));
            AlterColumn("dbo.Time", "Status", c => c.String());
            AlterColumn("dbo.Time", "Estadio", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Time", "Estado", c => c.String(nullable: false, maxLength: 2));
            AlterColumn("dbo.Time", "Nome", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Time", "CorUniforme");
            DropColumn("dbo.Classificacao", "SaldoGols");
        }
    }
}
