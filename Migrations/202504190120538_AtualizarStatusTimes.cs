namespace LigaTabajara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizarStatusTimes : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                UPDATE t
                SET t.Status = CASE 
                    WHEN (SELECT COUNT(*) FROM Jogador j WHERE j.TimeId = t.Id) >= 30 
                         AND (SELECT COUNT(*) FROM ComissaoTecnica ct WHERE ct.TimeId = t.Id) >= 5 
                    THEN 0 -- APTO
                    ELSE 1 -- NAO_APTO
                END
                FROM Time t
            ");
        }
        
        public override void Down()
        {
            Sql("UPDATE Time SET Status = 0");
        }
    }
}
