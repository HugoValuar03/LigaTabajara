namespace LigaTabajara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrecaoTime : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classificacaos", "TimeId", "dbo.Times");
            DropForeignKey("dbo.Jogadors", "TimeId", "dbo.Times");
            DropForeignKey("dbo.Gols", "JogadorId", "dbo.Jogadors");
            DropForeignKey("dbo.Gols", "PartidaId", "dbo.Partidas");
            DropForeignKey("dbo.Partidas", "TimeCasaId", "dbo.Times");
            DropForeignKey("dbo.Partidas", "TimeForaId", "dbo.Times");
            RenameTable(name: "dbo.Classificacaos", newName: "Classificacao");
            RenameTable(name: "dbo.Times", newName: "Time");
            RenameTable(name: "dbo.ComissaoTecnicas", newName: "ComissaoTecnica");
            RenameTable(name: "dbo.Jogadors", newName: "Jogador");
            RenameTable(name: "dbo.Gols", newName: "Gol");
            RenameTable(name: "dbo.Partidas", newName: "Partida");
            AddColumn("dbo.Classificacao", "SaldoGols", c => c.Int(nullable: false));
            AddColumn("dbo.Partida", "Time_Id", c => c.Int());
            AddColumn("dbo.Partida", "Time_Id1", c => c.Int());
            AlterColumn("dbo.Time", "Nome", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Time", "Estado", c => c.String(nullable: false, maxLength: 2));
            AlterColumn("dbo.Time", "Estadio", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ComissaoTecnica", "Nome", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Jogador", "Nome", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Jogador", "Nacionalidade", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Partida", "Estadio", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Partida", "Time_Id");
            CreateIndex("dbo.Partida", "Time_Id1");
            AddForeignKey("dbo.Partida", "Time_Id", "dbo.Time", "Id");
            AddForeignKey("dbo.Partida", "Time_Id1", "dbo.Time", "Id");
            AddForeignKey("dbo.Classificacao", "TimeId", "dbo.Time", "Id");
            AddForeignKey("dbo.Jogador", "TimeId", "dbo.Time", "Id");
            AddForeignKey("dbo.Gol", "JogadorId", "dbo.Jogador", "Id");
            AddForeignKey("dbo.Gol", "PartidaId", "dbo.Partida", "Id");
            AddForeignKey("dbo.Partida", "TimeCasaId", "dbo.Time", "Id");
            AddForeignKey("dbo.Partida", "TimeForaId", "dbo.Time", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Partida", "TimeForaId", "dbo.Time");
            DropForeignKey("dbo.Partida", "TimeCasaId", "dbo.Time");
            DropForeignKey("dbo.Gol", "PartidaId", "dbo.Partida");
            DropForeignKey("dbo.Gol", "JogadorId", "dbo.Jogador");
            DropForeignKey("dbo.Jogador", "TimeId", "dbo.Time");
            DropForeignKey("dbo.Classificacao", "TimeId", "dbo.Time");
            DropForeignKey("dbo.Partida", "Time_Id1", "dbo.Time");
            DropForeignKey("dbo.Partida", "Time_Id", "dbo.Time");
            DropIndex("dbo.Partida", new[] { "Time_Id1" });
            DropIndex("dbo.Partida", new[] { "Time_Id" });
            AlterColumn("dbo.Partida", "Estadio", c => c.String());
            AlterColumn("dbo.Jogador", "Nacionalidade", c => c.String());
            AlterColumn("dbo.Jogador", "Nome", c => c.String());
            AlterColumn("dbo.ComissaoTecnica", "Nome", c => c.String());
            AlterColumn("dbo.Time", "Estadio", c => c.String());
            AlterColumn("dbo.Time", "Estado", c => c.String());
            AlterColumn("dbo.Time", "Nome", c => c.String());
            DropColumn("dbo.Partida", "Time_Id1");
            DropColumn("dbo.Partida", "Time_Id");
            DropColumn("dbo.Classificacao", "SaldoGols");
            AddForeignKey("dbo.Partidas", "TimeForaId", "dbo.Times", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Partidas", "TimeCasaId", "dbo.Times", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Gols", "PartidaId", "dbo.Partidas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Gols", "JogadorId", "dbo.Jogadors", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Jogadors", "TimeId", "dbo.Times", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Classificacaos", "TimeId", "dbo.Times", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Partida", newName: "Partidas");
            RenameTable(name: "dbo.Gol", newName: "Gols");
            RenameTable(name: "dbo.Jogador", newName: "Jogadors");
            RenameTable(name: "dbo.ComissaoTecnica", newName: "ComissaoTecnicas");
            RenameTable(name: "dbo.Time", newName: "Times");
            RenameTable(name: "dbo.Classificacao", newName: "Classificacaos");
        }
    }
}
