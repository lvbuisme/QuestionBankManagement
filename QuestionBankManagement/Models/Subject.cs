using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankManagement.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(32)]
        public string SubjectName { get; set; }
        [InverseProperty("Subject")]
        public ICollection<ExaminationQuestion> ExaminationQuestions { get; set; }
        [InverseProperty("Subject")]
        public ICollection<TestPaper> TestPapers { get; set; }
    }
}
