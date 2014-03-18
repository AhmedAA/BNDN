namespace ISeeN_DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MainProgram : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Type = c.Int(nullable: false),
                        ReleaseDate = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Potatoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EncPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reminders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MediaId = c.Int(nullable: false),
                        Title = c.String(),
                        Message = c.String(),
                        DateSent = c.String(),
                        DateReceived = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        MediaId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.MediaId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Statistics");
            DropTable("dbo.Reminders");
            DropTable("dbo.Potatoes");
            DropTable("dbo.Media");
        }
    }
}
