using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankManagement.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(32)]
        public string RoleName { get; set; }
        [InverseProperty("Role")]
        public ICollection<User> Users { get; set; }
        [InverseProperty("Role")]
        public ICollection<RoleRight> RoleRights { get; set; }
    }
}
