using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankManagement.Models
{
    public class OptionItem
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(128)]
        public string OptionContent { get; set; }

        public int ExaminationQuestionsId { get; set; }
        [ForeignKey("ExaminationQuestionsId")]
        [InverseProperty("OptionItems")]
        public ExaminationQuestion ExaminationQuestion { get; set; }
    }
}
