using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StyleTee.Controllers
{
    public class AccessController : Controller
    {
        public AccessController()
        {

        }

        public IActionResult DangNhap()
        {
            return View();
        }

        public IActionResult DangKy()
        {
            return View();
        }
    }
}

