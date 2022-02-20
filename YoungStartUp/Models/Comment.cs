using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YoungStartUp.Models
{
    public class Comment
    {
        [Key]
        public int IdComment { get; set; }
        public string Content { get; set; }
        public int LogInUser_IdLogInUser { get; set; }
        public int Project_IdProject { get; set; }
    }
}