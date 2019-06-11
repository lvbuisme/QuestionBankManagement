using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankManagement.Models
{
    public class QuestionType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(32)]
        public string QuestionTypeName { get; set; }
        public bool IsFixedScore { get; set; }
        public bool HasOption { get; set; }
        public int Score { get; set; }
        [InverseProperty("QuestionType")]
        public ICollection<ExaminationQuestion> ExaminationQuestions { get; set; }

        [InverseProperty("QuestionType")]
        public ICollection<PaperQuestionType> PaperQuestionTypes { get; set; }
    }
}
