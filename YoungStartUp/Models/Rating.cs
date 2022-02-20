using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YoungStartUp.Models
{
    public class Rating
    {
        [Key]
        public int IdRating { get; set; }
        public ProjectRate Ratings { get; set; }
        public int LogInUser_IdLogInUser { get; set; }
        public int Project_IdProject { get; set; }

    }
}
