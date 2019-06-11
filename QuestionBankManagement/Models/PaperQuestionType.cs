using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankManagement.Models
{
    public class PaperQuestionType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(32)]
        public string PaperQuestionTitle { get; set; }

        public int QuestionTypeId { get; set; }
        [ForeignKey("QuestionTypeId")]
        [InverseProperty("PaperQuestionTypes")]
        public QuestionType QuestionType { get; set; }
        public int TestPaperId { get; set; }
        [ForeignKey("TestPaperId")]
        [InverseProperty("PaperQuestionTypes")]
        public TestPaper TestPaper { get; set; }

        [InverseProperty("PaperQuestionType")]
        public ICollection<ExaminationQuestionPaperType> ExaminationQuestionPaperTypes { get; set; }
    }
}
