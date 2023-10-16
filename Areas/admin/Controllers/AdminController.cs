using Microsoft.AspNetCore.Mvc;

namespace LabSpace2.Areas.admin.Controllers
{
    public class AdminController : Controller
    {
        [Area("Admin")]
        public IActionResult Index(){
            return View();  
        }
    }
}