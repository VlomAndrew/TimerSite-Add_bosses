namespace FirstProject.BossContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BossViewModels", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BossViewModels", "Color");
        }
    }
}
