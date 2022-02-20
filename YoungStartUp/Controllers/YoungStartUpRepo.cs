using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoungStartUp.Models;

namespace YoungStartUp.Controllers
{
    public class YoungStartUpRepo : IYoungStartUpRepo
    {
        private readonly YoungStartUpDbContext _context;
        public YoungStartUpRepo(YoungStartUpDbContext context)
        {
            _context = context;
        }

        public void DeleteCommentFromDatabase(Comment model)
        {
            _context.Comment.Remove(model);
            _context.SaveChanges();
        }
        public void DeleteProjectFromDatabase(Project model)
        {
            _context.Project.Remove(model);
            _context.SaveChanges();
        }
        public void DeleteRatingFromDatabase(Rating model)
        {
            _context.Rating.Remove(model);
            _context.SaveChanges();
        }
        public void DeleteUserFromDatabase(LogInUser model)
        {
            _context.LogInUser.Remove(model);
            _context.SaveChanges();
        }
        public void AddUserToDatabase(LogInUser model)
        {
            _context.LogInUser.Add(model);
            _context.SaveChanges();
        }
        public bool AddProjectToDatabase(Project model)
        {
            try
            {
                _context.Project.Add(model);
                _context.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                return false;
            }
           return true;
        }
        public bool AddCommentToDatabase(Comment model)
        {
            try
            {
                _context.Comment.Add(model);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
            return true;

        }

    public bool CheckIfCanAddUser(string login,string email)
        {
            var user = _context.LogInUser.Where(p => p.Username == login).ToList();
            var mail =  _context.LogInUser.Where(p => p.Email == email).ToList();
            if (user.Count == 0 && mail.Count==0)
                return true;
            return false;
        }
        public bool CheckIfUserExists(string login, string password)
        {
           var user= _context.LogInUser.Where(p => p.Username == login).Where(p => p.Password == password).ToList();
            if (user.Count == 0)
                return false;
            return true;
        }
        public bool CheckAdmin(string login,string passowrd)
        {
            try
            {
                var admin = _context.Admin.First(p => p.Username == login && p.Password == passowrd);
            }
            catch(InvalidOperationException ex)
            {
                return false;
            }
            return true;

        }

        public List<Project> GetProjects()
        {
            var projects = _context.Project.ToList();
            return projects;
        }
        public Project GetProject(int id)
        {
            try
            {
                return _context.Project.First(p => p.IdProject==id);
            }
            catch (Exception)
            {
                return null;
            }

        }
        public List<Project> GetProjects(int userId)
        {
            return _context.Project.Where(p => p.LogInUser_IdLogInUser == userId).ToList();
        }
        public LogInUser GetUser(string login)
        {
            try
            {
                return _context.LogInUser.First(p => p.Username == login);
            }
            catch(Exception)
            {
                return null;
            }
        }
        public LogInUser GetUser(int id)
        {
            try
            {
                return _context.LogInUser.First(p => p.IdLogInUser == id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<LogInUser> GetUsers()
        {
            var users = _context.LogInUser.ToList();
            return users;
        }
        public List<Comment> GetComments(int id)
        {
            return _context.Comment.Where(c => c.Project_IdProject == id).ToList();
        }

        public List<Comment> GetComments()
        {
            var comments = _context.Comment.ToList();
            return comments;
        }
        public List<Project> GetSearchedProjects(string title, string description)
        {
            var projects = _context.Project.Where(p => p.Title == title).ToList();
            if (projects.Count == 0)
            {
                projects = _context.Project.Where(p => p.Description == description).ToList();
                return projects;
            }
            return projects;
        }

        public List<Project> SortDescDateProjects()
        {
            var projects = _context.Project.OrderByDescending(p => p.AddedDate).ToList();
            return projects;
        }

        public List<Project> SortAscDateProject()
        {
            var projects = _context.Project.OrderBy(p => p.AddedDate).ToList();
            return projects;
        }

        public List<Project> SortDescendingAlphabeticProjects()
        {
            var projects = _context.Project.OrderByDescending(p => p.Title).ToList();
            return projects;
        }
        public List<Project> SortAscendingAlphabeticProjects()
        {
            var projects = _context.Project.OrderBy(p => p.Title).ToList();
            return projects;
        }
        public List<Rating> GetRating(int projectId)
        {
           return  _context.Rating.Where(p => p.Project_IdProject == projectId).ToList();
        }
        public List<Rating> GetRating()
        {
            return _context.Rating.ToList();
        }
        public bool AddRating(Rating model)
        {
            try
            {
                
                _context.Rating.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }

        }
        public bool UpdateRating(int projectId,float ret)
        {
            _context.Project.First(p => p.IdProject == projectId).Rating = ret;
            _context.SaveChanges();
            return true;
        }
    }
}
