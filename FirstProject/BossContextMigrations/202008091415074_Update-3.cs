namespace FirstProject.BossContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BossViewModels", "KdTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BossViewModels", "KdTime", c => c.Int(nullable: false));
        }
    }
}
