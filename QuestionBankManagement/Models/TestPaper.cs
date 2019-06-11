using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankManagement.Models
{
    public class TestPaper
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(128)]
        public string Title { get; set; }
        [MaxLength(128)]
        public string Subtitle { get; set; }
        public bool IsFinish { get; set; }
        [InverseProperty("TestPaper")]
        public ICollection<PaperQuestionType> PaperQuestionTypes { get; set; }

        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        [InverseProperty("TestPapers")]
        public Subject Subject { get; set; }
    }
}
