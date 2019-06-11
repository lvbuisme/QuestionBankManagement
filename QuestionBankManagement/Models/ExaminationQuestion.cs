using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankManagement.Models
{
    public class ExaminationQuestion
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(1000)]
        public string Content { get; set; }
        public int Score { get; set; }
        public int DifficultyGrade { get; set; }

        public int KnowledgePointId { get; set; }
        [ForeignKey("KnowledgePointId")]
        [InverseProperty("ExaminationQuestions")]
        public KnowledgePoint KnowledgePoint { get; set; }

        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        [InverseProperty("ExaminationQuestions")]
        public Subject Subject { get; set; }

        public int QuestionTypeId { get; set; }
        [ForeignKey("QuestionTypeId")]
        [InverseProperty("ExaminationQuestions")]
        public QuestionType QuestionType { get; set; }

        [InverseProperty("ExaminationQuestion")]
        public ICollection<OptionItem> OptionItems { get; set; }

        [InverseProperty("ExaminationQuestion")]
        public ICollection<ExaminationQuestionPaperType> ExaminationQuestionPaperTypes { get; set; }

    }
}
