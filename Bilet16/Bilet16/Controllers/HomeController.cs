using Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bilet16.Controllers
{
    public class HomeController : Controller
    {
        ITeamService _teamService;

        public HomeController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public IActionResult Index()
        {
            var members = _teamService.GetAll();
            return View(members);
        }

       
    }
}
