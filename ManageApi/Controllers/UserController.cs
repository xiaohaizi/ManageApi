using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManageApi.Base;
using ManageApi.Common;
using ManageApi.Filter;
using ManageApi.Model;
using ManageApi.Model.ModelBll;
using ManageApi.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace ManageApi.Controllers
{   
    public class UserController : CommonController
    {
        [HttpGet]
        public IActionResult Get(int p=1) {
            Status<PageReturn<AdminRoleViewModel>> status = new Status<PageReturn<AdminRoleViewModel>>();
            Admin admin = new Admin();
            status .Data= admin.GetPageList(a => a.ID > 0,p);
            status.Code = 100;
            return Json(status);
        }
       
        [HttpPost("adduser")]
        public IActionResult AddUser(string user_name,string pwd,int role_id) {
            Status<string> status = new Status<string>();
            Admin users = new Admin();
            users.Username = user_name;         
            users.Addtime = DateTime.Now;
            users.Encrypt = Business.GenerateRandom();
            users.Logintime = DateTime.Now.AddDays(-30);
            string userpwd = Business.MD5Encrypt(pwd, users.Encrypt);
            users.Userpwd = userpwd;
            users.RoleID = role_id;
            users.Guid = Guid.NewGuid().ToString("N");
            bool user=MysqlDbContext.Instance.Admin.Insert(users);
            status.Code = 300;
            status.Msg = "用户添加失败";
            if (user) {
                status.Code = 200;
                status.Msg = "用户添加成功";
            }
            return Json(status);
        }
    }
}