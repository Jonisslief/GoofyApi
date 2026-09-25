using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; } = 1;
        public string Name { get; set; } = "User";
    }
}
