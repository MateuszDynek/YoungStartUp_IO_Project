using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using YoungStartUp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace YoungStartUp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IYoungStartUpRepo _repo;
        public LoginController(IYoungStartUpRepo repo)
        {
            _repo = repo;
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(LogInUser model)
        {
            string login = model.Username;
            string password = model.Password;
            var check = _repo.CheckIfUserExists(login, password);
            if(check)
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

    }
}
