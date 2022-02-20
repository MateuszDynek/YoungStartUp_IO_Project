using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoungStartUp.Models
{
    public class UserProjectPageModel
    {
        public UserProjectPageModel()
        {
        }

        public UserProjectPageModel(Project project, List<Comment> comments)
        {
            this.project = project;
            this.comments = comments;
        }

        public Project project { get; set; }
        public Comment comment { get; set; }
        public List<Comment> comments { get; set; }
    }
}
