using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoungStartUp.Models;

namespace YoungStartUp.Controllers
{
    public interface IYoungStartUpRepo
    {
        public bool CheckIfUserExists(string login, string password);
        public bool CheckIfCanAddUser(string login, string email);
        public void AddUserToDatabase(LogInUser model);
        public bool AddProjectToDatabase(Project model);
        public bool AddCommentToDatabase(Comment model);
        public bool CheckAdmin(string login, string passowrd);
        public List<Project> GetProjects();
        public List<Project> GetProjects(int userId);
        public Project GetProject(int id);
        public LogInUser GetUser(string login);
        public LogInUser GetUser(int id);
        public List<LogInUser> GetUsers();
        public List<Comment> GetComments(int id);
        public List<Comment> GetComments();
        public void DeleteCommentFromDatabase(Comment model);
        public void DeleteProjectFromDatabase(Project model);
        public void DeleteUserFromDatabase(LogInUser model);
        public List<Project> SortAscDateProject();
        public List<Project> SortDescDateProjects();
        public List<Project> SortDescendingAlphabeticProjects();
        public List<Project> SortAscendingAlphabeticProjects();
        public List<Project> GetSearchedProjects(string title, string description);
        public List<Rating> GetRating(int projectId);
        public List<Rating> GetRating();
        public bool AddRating(Rating model);
        public bool UpdateRating(int projectId,float ret);
        public void DeleteRatingFromDatabase(Rating model);

    }
}
