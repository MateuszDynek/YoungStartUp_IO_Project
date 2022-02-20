using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YoungStartUp.Models
{
    public class Payments
    {
        [Key]
        public int IdPayments { get; set; }
        public float Amount { get; set; }
        public float Confirm { get; set; }
        public int LogInUser_IdLogInUser { get; set; }
        public int Project_IdProject { get; set; }
        public float Paid { get; set; }
    }
}

