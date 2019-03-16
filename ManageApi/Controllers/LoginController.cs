using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManageApi.Base;
using ManageApi.Common;
using ManageApi.Filter;
using ManageApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace ManageApi.Controllers
{

    public class LoginController : CommonController
    {
        [HttpGet]
        public JsonResult Get()
        {
            var list = MysqlDbContext.Instance.Menu.GetList();
            string timestamp = Business.GetTimestamp(DateTime.Now);
            DateTime time = Business.GetDateTime(timestamp);
            return Json(list);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(string username, string userpwd)
        {
            Status<Dictionary<string, string>> status = new Status<Dictionary<string, string>>();
            var user = MysqlDbContext.Instance.Admin.GetSingle(x => x.Username == username);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (user == null)
            {
                status.Code = 100;
                status.Msg = "用户不存在";
                return Json(status);
            }
            if (!Business.CheckMD5(userpwd, user.Userpwd, user.Encrypt))
            {
                status.Code = 100;
                status.Msg = "用户密码错误";
                return Json(status);
            }
            string user_key = Business.EncryptDES(user.ID.ToString()+"_"+user.Guid+"_"+Business.GetTimestamp(DateTime.Now) , user.Encrypt);
            status.Code = 200;
            status.Msg = "登录成功";
            user_key= user_key + "_" + user.Encrypt;
            user.Userpwd = "";
            //Json(user).ToString();
            HttpContext.Session.SetInt32("m_user_id", user.ID);
            status.Data = dic;
            return Json(status);

        }



    }
}