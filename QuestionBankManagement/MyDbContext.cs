using MySql.Data.Entity;
using QuestionBankManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankManagement
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MyDbContext : DbContext
    {
        public MyDbContext()
          : base("name=MyContext")
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<ExaminationQuestion> ExaminationQuestion { get; set; }
        public virtual DbSet<ExaminationQuestionPaperType> ExaminationQuestionPaperType { get; set; }
        public virtual DbSet<KnowledgePoint> KnowledgePoint { get; set; }
        public virtual DbSet<OptionItem> OptionItem { get; set; }
        public virtual DbSet<PaperQuestionType> PaperQuestionType { get; set; }
        public virtual DbSet<QuestionType> QuestionType { get; set; }
        public virtual DbSet<RoleRight> RoleRight { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<TestPaper> TestPaper { get; set; }

    }
}
