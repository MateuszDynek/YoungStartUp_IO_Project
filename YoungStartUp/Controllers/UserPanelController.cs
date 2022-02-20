using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using YoungStartUp.Models;
namespace YoungStartUp.Controllers
{
    public class UserPanelController : Controller
    {
        private readonly IYoungStartUpRepo _repo;

        public UserPanelController(IYoungStartUpRepo repo)
        {
            _repo = repo;
        }
        public IActionResult UserPanel()
        {
            return View();
        }
        
        public IActionResult AddProject()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProject(Project model)
        {
            try
            {
                if(model.ShareType==(ShareType)1)
                {
                    model.Price = 0;
                }

                model.AddedDate = DateTime.Now;
                model.LogInUser_IdLogInUser = _repo.GetUser(HttpContext.Session.GetString("username")).IdLogInUser;

                if (_repo.AddProjectToDatabase(model))
                    ViewBag.success = "Dodano";
                else
                    ViewBag.success = "Nie udało się dodać projektu";
            }
            catch(NullReferenceException)
            {
                ViewBag.success = "Wystąpił problem z sesją";
            }
            
               
            return View();
        }
        
        public IActionResult ShowProjects()
        {
            var projects = _repo.GetProjects(_repo.GetUser(HttpContext.Session.GetString("username")).IdLogInUser);
            //// do widokow czesciowych
            var model = new ProjectList(projects);

            /*
            int lel = _repo.GetUser(HttpContext.Session.GetString("username")).IdLogInUser;
            var projects = _repo.GetProjects();
            ViewBag.Projects = projects;
            ViewBag.ProjectsSize = projects.Count;
            ViewBag.UserId = lel;*/
            var users = _repo.GetUsers();
            ViewBag.Users = users;
            ViewBag.UsersSize = users.Count;
            return View(model);
        }
        
    }
}
