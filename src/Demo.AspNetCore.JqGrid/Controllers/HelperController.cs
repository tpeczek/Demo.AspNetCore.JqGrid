using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Demo.AspNetCore.JqGrid.Controllers
{
    public class HelperController : Controller
    {
        #region Actions
        public IActionResult Basics()
        {
            return View();
        }
        #endregion
    }
}
