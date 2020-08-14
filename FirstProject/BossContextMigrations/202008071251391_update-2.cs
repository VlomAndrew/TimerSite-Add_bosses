namespace FirstProject.BossContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BossViewModels", "LastTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BossViewModels", "LastTime", c => c.DateTime());
        }
    }
}
