using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManageApi.Base;
using ManageApi.Common;
using ManageApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace ManageApi.Controllers
{
    
    public class ManageController : CommonController
    {
        [HttpPost("add")]
        public IActionResult Add( string user_name,  string pwd)
        {
            Status<Dictionary<string,string>> status = new Status<Dictionary<string, string>>();
            Admin admin = new Admin();
            admin.Addtime = DateTime.Now;
            admin.Encrypt = Business.GenerateRandom();
            string upwd = Business.MD5Encrypt(pwd, admin.Encrypt);
            admin.Userpwd = upwd;
            admin.Username = user_name;
            admin.Logintime = DateTime.Now;
            admin.Guid=Guid.NewGuid().ToString("N");
            status.Code = 300;
            status.Msg = "添加失败";
            if (admin.Add()) {
                status.Code = 200;
                status.Msg = "添加成功";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("username", user_name);
                dic.Add("addtime", admin.Addtime.ToString("yyyy-mm-dd HH:mm:ss"));
                status.Data = dic;
            }
            return Json(status);
        }
    }
}