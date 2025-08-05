using Microsoft.AspNetCore.Mvc;

namespace AngularWithASP.Server.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
