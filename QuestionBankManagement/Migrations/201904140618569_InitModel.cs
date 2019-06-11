namespace QuestionBankManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExaminationQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 300, storeType: "nvarchar"),
                        Score = c.Int(nullable: false),
                        KnowledgePointId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        QuestionTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KnowledgePoints", t => t.KnowledgePointId, cascadeDelete: true)
                .ForeignKey("dbo.QuestionTypes", t => t.QuestionTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.KnowledgePointId)
                .Index(t => t.SubjectId)
                .Index(t => t.QuestionTypeId);
            
            CreateTable(
                "dbo.ExaminationQuestionPaperTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExaminationQuestionId = c.Int(nullable: false),
                        PaperQuestionTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaperQuestionTypes", t => t.PaperQuestionTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ExaminationQuestions", t => t.ExaminationQuestionId, cascadeDelete: true)
                .Index(t => t.ExaminationQuestionId)
                .Index(t => t.PaperQuestionTypeId);
            
            CreateTable(
                "dbo.PaperQuestionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaperQuestionTitle = c.String(maxLength: 32, storeType: "nvarchar"),
                        QuestionTypeId = c.Int(nullable: false),
                        TestPaperId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionTypes", t => t.QuestionTypeId, cascadeDelete: true)
                .ForeignKey("dbo.TestPapers", t => t.TestPaperId, cascadeDelete: true)
                .Index(t => t.QuestionTypeId)
                .Index(t => t.TestPaperId);
            
            CreateTable(
                "dbo.QuestionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionTypeName = c.String(maxLength: 32, storeType: "nvarchar"),
                        IsFixedScore = c.Boolean(nullable: false),
                        HasOption = c.Boolean(nullable: false),
                        Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TestPapers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 128, storeType: "nvarchar"),
                        Subtitle = c.String(maxLength: 128, storeType: "nvarchar"),
                        IsFinish = c.Boolean(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(maxLength: 32, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KnowledgePoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KnowledgePointName = c.String(maxLength: 32, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OptionItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OptionContent = c.String(maxLength: 32, storeType: "nvarchar"),
                        ExaminationQuestionsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExaminationQuestions", t => t.ExaminationQuestionsId, cascadeDelete: true)
                .Index(t => t.ExaminationQuestionsId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 32, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleRights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RightName = c.String(maxLength: 32, storeType: "nvarchar"),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sex = c.Int(nullable: false),
                        UserName = c.String(maxLength: 32, storeType: "nvarchar"),
                        Password = c.String(maxLength: 32, storeType: "nvarchar"),
                        Account = c.String(maxLength: 32, storeType: "nvarchar"),
                        MobilePhone = c.String(maxLength: 32, storeType: "nvarchar"),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.RoleRights", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ExaminationQuestions", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.ExaminationQuestions", "QuestionTypeId", "dbo.QuestionTypes");
            DropForeignKey("dbo.OptionItems", "ExaminationQuestionsId", "dbo.ExaminationQuestions");
            DropForeignKey("dbo.ExaminationQuestions", "KnowledgePointId", "dbo.KnowledgePoints");
            DropForeignKey("dbo.ExaminationQuestionPaperTypes", "ExaminationQuestionId", "dbo.ExaminationQuestions");
            DropForeignKey("dbo.ExaminationQuestionPaperTypes", "PaperQuestionTypeId", "dbo.PaperQuestionTypes");
            DropForeignKey("dbo.PaperQuestionTypes", "TestPaperId", "dbo.TestPapers");
            DropForeignKey("dbo.TestPapers", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.PaperQuestionTypes", "QuestionTypeId", "dbo.QuestionTypes");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.RoleRights", new[] { "RoleId" });
            DropIndex("dbo.OptionItems", new[] { "ExaminationQuestionsId" });
            DropIndex("dbo.TestPapers", new[] { "SubjectId" });
            DropIndex("dbo.PaperQuestionTypes", new[] { "TestPaperId" });
            DropIndex("dbo.PaperQuestionTypes", new[] { "QuestionTypeId" });
            DropIndex("dbo.ExaminationQuestionPaperTypes", new[] { "PaperQuestionTypeId" });
            DropIndex("dbo.ExaminationQuestionPaperTypes", new[] { "ExaminationQuestionId" });
            DropIndex("dbo.ExaminationQuestions", new[] { "QuestionTypeId" });
            DropIndex("dbo.ExaminationQuestions", new[] { "SubjectId" });
            DropIndex("dbo.ExaminationQuestions", new[] { "KnowledgePointId" });
            DropTable("dbo.Users");
            DropTable("dbo.RoleRights");
            DropTable("dbo.Roles");
            DropTable("dbo.OptionItems");
            DropTable("dbo.KnowledgePoints");
            DropTable("dbo.Subjects");
            DropTable("dbo.TestPapers");
            DropTable("dbo.QuestionTypes");
            DropTable("dbo.PaperQuestionTypes");
            DropTable("dbo.ExaminationQuestionPaperTypes");
            DropTable("dbo.ExaminationQuestions");
        }
    }
}
