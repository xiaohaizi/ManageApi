using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManageApi.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace ManageApi.Base
{
    
    [LoginRequiredAttribute(Check = true)]
    public class UsersController : Controller
    {
        public int BaseUserID = 0;
        public UsersController()
        {
            BaseUserID= HttpContext.Session.GetInt32("m_user_id")==null?0:Convert.ToInt32(HttpContext.Session.GetInt32("m_user_id"));
        }
    }
}