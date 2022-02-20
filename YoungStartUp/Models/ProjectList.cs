using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoungStartUp.Models
{
    public class ProjectList
    {
        public ProjectList(List<Project> projectsList)
        {
            ProjectsList = projectsList;
        }

        public  List<Project> ProjectsList { get; set; }
        
    }
}
