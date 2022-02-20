using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoungStartUp.Models
{
    public class UsersList
    {
        public UsersList(List<LogInUser> usersList)
        {
            this.usersList = usersList;
        }

        public List<LogInUser> usersList { get; set; }

    }
}
