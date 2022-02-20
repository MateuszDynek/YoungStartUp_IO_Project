using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoungStartUp.Models;

namespace YoungStartUp.Controllers
{
    
    public class BrowseProjectsController : Controller
    {
        private readonly IYoungStartUpRepo _repo;
        public BrowseProjectsController(IYoungStartUpRepo repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IActionResult Search(string title, string description)
        {
            var projects = _repo.GetSearchedProjects(title, description);
            ProjectList project = new ProjectList(projects);
            return View("ProjectsMainPage", project);
        }
        [HttpGet]
        public IActionResult SortAscDate()
        {
            var projects = _repo.SortAscDateProject();
            ProjectList project = new ProjectList(projects);
            return View("ProjectsMainPage", project);
        }
        public IActionResult SortDescDate()
        {
            var projects = _repo.SortDescDateProjects();
            ProjectList project = new ProjectList(projects);
            return View("ProjectsMainPage", project);
        }
        public IActionResult SortDescAlphabetic()
        {
            var projects = _repo.SortDescendingAlphabeticProjects();
            ProjectList project = new ProjectList(projects);
            return View("ProjectsMainPage", project);
        }
        public IActionResult SortAscAlphabetic()
        {
            var projects = _repo.SortAscendingAlphabeticProjects();
            ProjectList project = new ProjectList(projects);
            return View("ProjectsMainPage", project);
        }
        public IActionResult ProjectsMainPage()
        {

            var projects = _repo.GetProjects();
            //// do widokow czesciowych
            var model = new ProjectList(projects);
          
            ////!!!!!
            var users = _repo.GetUsers();
            ViewBag.Users = users;
            ViewBag.UsersSize = users.Count;
            ViewBag.Projects = projects;
            ViewBag.ProjectsSize = projects.Count;
            
            return View(model);
        }
        [HttpPost]
        public IActionResult UserProjectPage(int idProject)
        {

            
            var project = _repo.GetProject(idProject);
            var comments = _repo.GetComments(idProject);
            
           UserProjectPageModel userProjectPage = new UserProjectPageModel(project,comments);
            
            ViewBag.User = _repo.GetUser(project.LogInUser_IdLogInUser);
            
            return View(userProjectPage);
        }
        [HttpPost]
        public IActionResult AddComment(UserProjectPageModel model)
        {
            Comment comment;
            comment = model.comment;
            comment.Project_IdProject = model.project.IdProject;
            ViewBag.User = _repo.GetUser(model.project.LogInUser_IdLogInUser);
            var user = _repo.GetUser(HttpContext.Session.GetString("username"));
            model.comments = _repo.GetComments(model.project.IdProject);
            if (user != null)
            {
                comment.LogInUser_IdLogInUser = user.IdLogInUser;
            }
            else//jezli sie jest nie zalogowanym 
            {
                
                ViewBag.CommentError = "Nie zalogowany";
                return View("UserProjectPage", model);
            }
            
            
            if(!_repo.AddCommentToDatabase(comment))//jakis inny blad
            {
                ViewBag.CommentError = "Błąd dodania";
            }
            else
            {
                ViewBag.CommentSuccess = "Dodano komentarz";
            }

            return LocalRedirect("~/");
        }
        public IActionResult AddScoreToProject(UserProjectPageModel model)
        {
            var rating = new Rating();
            rating.Project_IdProject = model.project.IdProject;
            rating.LogInUser_IdLogInUser = _repo.GetUser(HttpContext.Session.GetString("username")).IdLogInUser;

            return View(rating);
        }
        public IActionResult AddScoreToProject2(Rating model)
        {
            _repo.AddRating(model);
            var rating = _repo.GetRating(model.Project_IdProject);
            int sum = 0;
            foreach(Rating r in rating)
            {
                sum += (int)r.Ratings;
            }
            if(rating.Count==0)
            {
                _repo.UpdateRating(model.Project_IdProject, sum);
            }
            else 
            {
                _repo.UpdateRating(model.Project_IdProject, sum / rating.Count);
            }
            

            var project = _repo.GetProject(model.Project_IdProject);
            var comments = _repo.GetComments(model.Project_IdProject);


            UserProjectPageModel userProjectPage = new UserProjectPageModel(project, comments);

            ViewBag.User = _repo.GetUser(project.LogInUser_IdLogInUser);

            return View("UserProjectPage", userProjectPage);
        }

    }
}
