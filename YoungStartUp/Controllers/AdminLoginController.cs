using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoungStartUp.Models;
using Microsoft.AspNetCore.Http;

namespace YoungStartUp.Controllers
{
    public class AdminLoginController : Controller
    {
        private readonly IYoungStartUpRepo _repo;
        public AdminLoginController(IYoungStartUpRepo repo)
        {
            _repo = repo;
        }
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminLogin(Admin model)
        {
            string login = model.Username;
            string password = model.Password;
            
            if (_repo.CheckAdmin(login, password))
            {
                HttpContext.Session.SetString("username", login);

                return LocalRedirect("~/");
            }
            else
            {
                ViewBag.error = "Nieprawidłowe dane";
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return LocalRedirect("~/");
        }

        public IActionResult AdminPanel()
        {
            return View();
        }
        public IActionResult AddAdmin()
        {
            return View();
        }
        
        public IActionResult BanUsers()
        {
            var users = _repo.GetUsers();
            //// do widokow czesciowych
            var model = new UsersList(users);

            ////!!!!!
            var projects = _repo.GetProjects();
            ViewBag.Users = users;
            ViewBag.UsersSize = users.Count;
            ViewBag.Projects = projects;
            ViewBag.ProjectsSize = projects.Count;
            return View(model);
        }
        [HttpPost]
        public IActionResult BanSelectedUser(LogInUser model )
        {
            int userId = model.IdLogInUser;
            var tempRating = new List<Rating>();
            var allRating = _repo.GetRating();
            var user = _repo.GetUser(userId);
            var users = _repo.GetUsers();
            var projects = _repo.GetProjects();
            var projectRating = new List<Rating>();
            var allComments = _repo.GetComments();
            var tempComments = new List<Comment>();
            var projectComments = new List<Comment>();
            var userProjects = new List<Project>();
            for (int i = 0; i < projects.Count; i++)
            {
                if(user.IdLogInUser == projects[i].LogInUser_IdLogInUser)
                {
                    userProjects.Add(projects[i]);
                }

            }
            for (int i = 0; i < userProjects.Count; i++)
            {
                tempComments = _repo.GetComments(userProjects[i].IdProject);
                for (int j = 0; j < tempComments.Count; j++)
                {
                    projectComments.Add(tempComments[j]);
                }
                
            }
            for (int i = 0; i < userProjects.Count; i++)
            {
                tempRating = _repo.GetRating(userProjects[i].IdProject);
                for (int j = 0; j < tempComments.Count; j++)
                {
                    projectRating.Add(tempRating[j]);
                }

            }
            for (int i = 0; i < allComments.Count; i++)
            {
                for (int j = 0; j < projectComments.Count; j++)
                {
                    if(projectComments[j] ==allComments[i])
                    {
                        _repo.DeleteCommentFromDatabase(projectComments[j]);
                    }
                }
            }
            for (int i = 0; i < allRating.Count; i++)
            {
                for (int j = 0; j < projectComments.Count; j++)
                {
                    if (projectComments[j] == allComments[i])
                    {
                        _repo.DeleteRatingFromDatabase(projectRating[j]);
                    }
                }
            }
            for (int i = 0; i < projects.Count; i++)
            {
                for (int j = 0; j < userProjects.Count; j++)
                {
                    if (userProjects[j] == projects[i])
                    {
                        _repo.DeleteProjectFromDatabase(userProjects[j]);
                    }
                }
            }
            for (int i = 0; i < users.Count; i++)
            {
                if (user == users[i])
                {
                    _repo.DeleteUserFromDatabase(user);
                }
            }

            return LocalRedirect("~/");
        }
    }
}
