namespace ORMFoy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ders",
                c => new
                {
                    DersID = c.Int(nullable: false, identity: true),
                    DersAd = c.String(nullable: false),
                    BolumID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.DersID)
                .ForeignKey("dbo.Bolum", t => t.BolumID, cascadeDelete: true)
                .Index(t => t.BolumID);

            // Diğer tablolar için benzer kodlar...
        }


        public override void Down()
        {
        }
    }
}
