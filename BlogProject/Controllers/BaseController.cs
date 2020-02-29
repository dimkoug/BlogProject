using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    public class BaseController : Controller
    {
        public void AddMessage(string Key, string Message)
        {
            TempData["key"] = Key;
            TempData["Message"] = Message;
        }
    }
}