namespace FirstProject.BossContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BossViewModels", "LastTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BossViewModels", "LastTime", c => c.DateTime(nullable: false));
        }
    }
}
