namespace QuestionBankManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitModel3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OptionItems", "OptionContent", c => c.String(maxLength: 128, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OptionItems", "OptionContent", c => c.String(maxLength: 32, storeType: "nvarchar"));
        }
    }
}
