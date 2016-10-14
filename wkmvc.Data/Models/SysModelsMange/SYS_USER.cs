using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace wkmvc.Data.Models
{
    public class SYS_USER
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string USERNAME { get; set; }
        [Required]
        [MaxLength(50)]
        public string PASSWORD { get; set; }
    }
}
