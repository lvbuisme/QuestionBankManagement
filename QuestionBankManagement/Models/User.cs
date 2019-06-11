using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankManagement.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int Sex { get; set; }
        [MaxLength(32)]
        public string UserName { get; set; }
        [MaxLength(32)]
        public string Password { get; set; }
        [MaxLength(32)]
        public string Account { get; set; }
        [MaxLength(32)]
        public string MobilePhone { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("Users")]
        public Role Role { get; set; }
    }
}
