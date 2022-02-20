using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YoungStartUp.Models
{
    public class Report
    {
        [Key]
        public int IdReport { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int LogInUser_IdLogInUser { get; set; }
        public int Comment_IdComment { get; set; }
        public int Project_IdProject { get; set; }
    }
}
