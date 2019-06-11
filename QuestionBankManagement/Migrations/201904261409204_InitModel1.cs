namespace QuestionBankManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExaminationQuestions", "DifficultyGrade", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExaminationQuestions", "DifficultyGrade");
        }
    }
}
