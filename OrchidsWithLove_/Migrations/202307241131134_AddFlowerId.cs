namespace OrchidsWithLove_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFlowerId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FlowersPhotoes", "FlowerId", c => c.Guid(nullable: false));
            DropColumn("dbo.Flowers", "FlowersPhotoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Flowers", "FlowersPhotoId", c => c.Guid(nullable: false));
            DropColumn("dbo.FlowersPhotoes", "FlowerId");
        }
    }
}
