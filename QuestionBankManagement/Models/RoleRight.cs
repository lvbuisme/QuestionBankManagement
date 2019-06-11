using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankManagement.Models
{
    public class RoleRight
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(32)]
        public string RightName { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("RoleRights")]
        public Role Role { get; set; }
    }
}
