namespace OrchidsWithLove_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFlowersPhotos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FlowersPhotoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Flowers", "FlowersPhotoId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Flowers", "FlowersPhotoId");
            DropTable("dbo.FlowersPhotoes");
        }
    }
}
