using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YoungStartUp.Models
{
    public class Project
    {
        [Key]
        public int IdProject { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AddedDate { get; set; }
        public double Rating { get; set; }
        public ShareType ShareType { get; set; }
        public Target Target { get; set; }
        public string GitLink { get; set; }
        public string ExeLink { get; set; }
        public string HostLink { get; set; }
        public int LogInUser_IdLogInUser { get; set; }
        public double Price { get; set; }
    }
    public enum Target
    {
        Game,
        IoT,
        Desktop_App,
        Web_App,
        Bot,
    }
    public enum ShareType
    {
        Paid,
        Free
    }
    public enum ProjectRate
    {
        Terrible=1,
        Bad=2,
        Decent=3,
        Good=4,
        Exexcellent=5
    }

}