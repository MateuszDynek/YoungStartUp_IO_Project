using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using YoungStartUp.Models;

namespace YoungStartUp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IYoungStartUpRepo _repo;
        public HomeController(IYoungStartUpRepo repo)
        {
            _repo = repo;
        }
        

        public IActionResult Index()
        {
            var projects = _repo.GetProjects();
            var users = _repo.GetUsers();
            int i = GetBestRatedProject(projects);
            int j = GetNewestProject(projects);
            int k = GetMostCommentedProject(projects);
            ViewBag.BestRatestP = GetBestRatedProject(projects);
            ViewBag.NewestP = GetNewestProject(projects);
            ViewBag.MCommentedP = GetMostCommentedProject(projects);
            ViewBag.Projects = projects;
            ViewBag.Users = users;
            ViewBag.UserBRP = GetProjectUser(i, projects,users);
            ViewBag.UserNP = GetProjectUser(j, projects,users);
            ViewBag.UserMCP = GetProjectUser(k, projects,users);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public int GetBestRatedProject(List<Project> projects)
        {
            int iterator = 0;
            double rate = 0;
            for (int i = 0; i < projects.Count; i++)
            {
                if(rate<projects[i].Rating)
                {
                    rate = projects[i].Rating;
                    iterator = i;
                }
            }
            return iterator;
        }
        public int GetNewestProject(List<Project> projects)
        {
            int iterator = 0;
            int id = 0;
            for (int i = 0; i < projects.Count; i++)
            {
                if (id < projects[i].Rating)
                {
                    id = projects[i].IdProject;
                    iterator = i;
                }
            }
            return iterator;
        }
        public int GetMostCommentedProject(List<Project> projects)
        {
            var comments = _repo.GetComments();
            int iterator = 0;
            int value = 0;
            int counter = 0;
            for (int i = 0; i < projects.Count; i++)
            {
                for (int j = 0; j < comments.Count; j++)
                {
                    if (projects[i].IdProject == comments[j].Project_IdProject)
                    {
                        counter++;
                    }

                }
                if(value<counter)
                {
                    value = counter;
                    iterator = i;
                }
                counter = 0;
            }
            return iterator;
        }
        private int GetProjectUser(int iterator, List<Project> projects, List<LogInUser> users)
        {
            int _iterator = 0;
            int i = projects[iterator].LogInUser_IdLogInUser;
            for (int j = 0; j < users.Count; j++)
            {
                if(i==users[j].IdLogInUser)
                {
                    _iterator = j;
                }    
            }
            return _iterator;
        }
        
    }
}
