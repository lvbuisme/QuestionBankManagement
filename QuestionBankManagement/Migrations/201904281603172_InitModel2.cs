namespace QuestionBankManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitModel2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ExaminationQuestions", "Content", c => c.String(maxLength: 1000, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ExaminationQuestions", "Content", c => c.String(maxLength: 300, storeType: "nvarchar"));
        }
    }
}
