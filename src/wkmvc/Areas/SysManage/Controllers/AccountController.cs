using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wkmvc.Data;
using Service.IService;
using wkmvc.Data.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace wkmvc.Areas.SysManage.Controllers
{
    [Area("SysManage")]
    public class AccountController : Controller
    {
        private readonly IUserManage _UserManage;
        public AccountController(IUserManage UserManage)
        {
            _UserManage = UserManage;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            HttpContext.Session.Remove("CurrentUser");
            return View();
        }
        [HttpPost]
        public IActionResult LoginAjax(LoginViewModel user)
        {
            //_UserManage.Login(user);
            if (_UserManage.Login(user))
            {
                var account = _UserManage.Get(P => P.USERNAME == user.userName);
                //HttpContext.Session.Set("CurrentUser", account);
            }
            
            return Json(_UserManage.Login(user));
        }

        public IActionResult Main()
        {
            return View();
        }
    }
}
