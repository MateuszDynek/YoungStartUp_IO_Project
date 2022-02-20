using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using YoungStartUp.Models;
using System.Web;

namespace YoungStartUp.Controllers
{

    public class ContactController : Controller
    {
        private readonly IYoungStartUpRepo _repo;
        public ContactController(IYoungStartUpRepo repo)
        {
            _repo = repo;
        }
        [HttpPost]
        public IActionResult Message(UserProjectPageModel model)
        {

            var model2 = new Message();
            model2.project = model.project;
            return View(model2);
        }

        [HttpPost]
        public IActionResult SendMessage(Message model)
        {
            var message = new MailMessage();
            message.From = new MailAddress("youngstartupcorporation@gmail.com", "youngstartupcorporation@gmail.com");
            message.To.Add(new MailAddress(_repo.GetUser(model.project.LogInUser_IdLogInUser).Email));
            message.Subject = "Wiadomosc do projektu: " + _repo.GetProject(model.project.IdProject).Title;

            message.Body = $"Nie odpowaidaj na tego maila!!!\n Widaomość od użytkownika:{_repo.GetUser(HttpContext.Session.GetString("username")).Username}\n " + model.content;
            var smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("youngstartupcorporation@gmail.com", "@youngstartupcorporation1");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Send(message);

            return LocalRedirect("~/");
            
        }
    }
}
