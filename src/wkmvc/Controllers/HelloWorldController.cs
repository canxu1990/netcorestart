using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace wkmvc.Controllers
{
    public class HelloWorldController : Controller
    {
        private readonly IUserManage _UserManage;
        public HelloWorldController(IUserManage UserManage)
        {
            _UserManage = UserManage;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.IS1 = _UserManage.TestOne();
            return View();
        }
    }
}
