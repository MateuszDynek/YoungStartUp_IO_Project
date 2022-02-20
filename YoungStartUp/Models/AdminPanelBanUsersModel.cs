using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoungStartUp.Models
{
    public class AdminPanelBanUsersModel
    {
        public AdminPanelBanUsersModel()
        {
        }

        public AdminPanelBanUsersModel(List<Project> projects, List<Comment> comments, LogInUser user)
        {
            this.projects = projects;
            this.comments = comments;
            this.user = user;
        }

        public LogInUser user { get; set; }
        public List<Comment> comments { get; set; }
        public List<Project> projects { get; set; }
    }
}
