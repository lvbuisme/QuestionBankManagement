namespace QuestionBankManagement.Migrations
{
    using QuestionBankManagement.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QuestionBankManagement.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
          
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(QuestionBankManagement.MyDbContext context)
        {
            context.Role.Add(new Models.Role()
            {
                Id = 1,
                RoleName = "系统管理员",
                RoleRights = new List<RoleRight>() {
                    new RoleRight() { RightName = "UserManager" },
                     new RoleRight() { RightName = "QuestionBankManager" },
                      new RoleRight() { RightName = "ExaminationPaperManager" },
                }
            });
            context.Role.Add(new Models.Role() { Id = 2, RoleName = "老师" });
            context.Role.Add(new Models.Role() { Id = 3, RoleName = "学生" });
            context.Subject.Add(new Models.Subject() { Id = 1,SubjectName="计算机" });
            context.User.Add(new Models.User() { Id = 2,  Account="lvbu", MobilePhone="123456", Password="123456", RoleId=1, UserName="吕布" });
            context.QuestionType.Add(new Models.QuestionType() {  QuestionTypeName="应用题",IsFixedScore=false, HasOption=false });
            context.SaveChanges();
         
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
