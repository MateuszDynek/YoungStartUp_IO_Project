using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoungStartUp.Models;

namespace YoungStartUp.Controllers
{
    public class SupportProjectsController : Controller
    {
        private readonly IYoungStartUpRepo _repo;
        public SupportProjectsController(IYoungStartUpRepo repo)
        {
            _repo = repo;
        }
        public IActionResult Support(UserProjectPageModel model)
        {
            return View(model);

        }
        public IActionResult Payment()
        {
            return View();
        }

    }
}
