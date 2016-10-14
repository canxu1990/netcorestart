using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.Cahce;

namespace wkmvc.Controllers
{
    public class HomeController : Controller
    {
        public readonly IUserManage _IUserManage;
        public readonly Service.ConfigServices.AppConfigurtaionServices _AppConfigurtaionServices;
        public HomeController(IUserManage IUserManage, Service.ConfigServices.AppConfigurtaionServices AppConfigurtaionServices)
        {
            _IUserManage = IUserManage;
            _AppConfigurtaionServices = AppConfigurtaionServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = _AppConfigurtaionServices.GetAppSettings<ApplicationConfiguration>("SiteBaseConfig").FileUpPath;
            ViewData["Message1"]=_AppConfigurtaionServices.GetAppSettings<ApplicationConfiguration>("SiteBaseConfig").DataBase;
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
