using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YoungStartUp.Models
{
    public class Admin
    {
        [Key]
        public int IdAdmin { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}