using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNotes.bl.Abstract.Helpers;
using WebNotes.bl.Interfaces;
using WebNotes.data;

namespace WebNotes.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private readonly IUserService userService;
        private readonly INoteService noteService;

        public HomeController(IUserService userService, INoteService noteService)
        {
            this.userService = userService;
            this.noteService = noteService;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}