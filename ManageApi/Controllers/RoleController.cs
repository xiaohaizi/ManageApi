using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManageApi.Base;
using ManageApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace ManageApi.Controllers
{
    public class RoleController : CommonController
    {
        [HttpGet]
        public IActionResult Get()
        {
            Status<List<Role>> status = new Status<List<Role>>();

            return Json(status);
        }
    }
}