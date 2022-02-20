using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoungStartUp.Models;

namespace YoungStartUp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IYoungStartUpRepo _repo;
        public RegisterController(IYoungStartUpRepo repo)
        {
            _repo = repo;
        }

        public IActionResult SignUp()
        {
            return View();

        }
        [HttpPost]
        public IActionResult SignUp(LogInUser model)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            var password = model.Password;
            if(password.Length<8)
            {
                ViewBag.Error = "Za krótkie hasło. Conajmniej 8 znaków";
                return View(model);
            }
            else if(regexItem.IsMatch(password))
            {
                ViewBag.Error = "Brak znaku specjalnego";
                return View(model);
            }
            var check = _repo.CheckIfCanAddUser(model.Username, model.Email);
            if(check)
            
             {
                
                _repo.AddUserToDatabase(model);
                ViewBag.Success = "Pomyslnie utworzono konto";
                return View(model);
            }
            else
            {
                ViewBag.Error = "Istnieje uzytkownik o podanych danych";
                return View(model);
            }
        }
    }
}
