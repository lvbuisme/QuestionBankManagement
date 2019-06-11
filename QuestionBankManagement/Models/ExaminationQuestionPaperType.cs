using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankManagement.Models
{
    public class ExaminationQuestionPaperType
    {
        [Key]
        public int Id { get; set; }

        public int ExaminationQuestionId { get; set; }
        [ForeignKey("ExaminationQuestionId")]
        [InverseProperty("ExaminationQuestionPaperTypes")]
        public ExaminationQuestion ExaminationQuestion { get; set; }

        public int PaperQuestionTypeId { get; set; }
        [ForeignKey("PaperQuestionTypeId")]
        [InverseProperty("ExaminationQuestionPaperTypes")]
        public PaperQuestionType PaperQuestionType { get; set; }

    }
}
