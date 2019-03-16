using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManageApi.Filter;
using Microsoft.AspNetCore.Mvc;

namespace ManageApi.Base
{
    [Route("api/[controller]")]
    [LoginRequiredAttribute(Check = false)]
    public class CommonController : Controller
    {
        public CommonController()
        {
          
        }
    }
}