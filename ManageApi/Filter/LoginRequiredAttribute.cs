using ManageApi.Common;
using ManageApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageApi.Filter
{
    /// <summary>
    /// 验证登录
    /// </summary>
    public class LoginRequiredAttribute : ActionFilterAttribute
    {
       public bool Check = true;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string user_key = "";
            if (Check)
            {
                var request = context.HttpContext.Request;
                if (request.Method == "GET")
                {
                    var query = request.Query;
                    if (query.ContainsKey("user_key"))
                    {
                        user_key = query["user_key"];
                    }
                }
                if (request.Method == "POST")
                {
                    var query = request.Form;
                    if (query.ContainsKey("user_key"))
                    {
                        user_key = query["user_key"];
                    }
                }
                if (string.IsNullOrWhiteSpace(user_key.Trim()))
                {
                    Status<string> status = new Status<string>();
                    status.Code = 300;
                    status.Msg = "请先登录";
                    status.Data = "";
                    context.Result = new JsonResult(status);
                    return;
                }
              
                var user_encrypt = user_key.Split('_');
                if (user_encrypt.Length < 2)
                {
                    Status<string> status = new Status<string>();
                    status.Code = 100;
                    status.Msg = "用户秘钥错误";
                    status.Data = "";
                    context.Result = new JsonResult(status);
                    return;
                }
               string user_str= Business.DecryptDES(user_encrypt[0], user_encrypt[1]);
               var user_arr = user_str.Split('_');
                if (user_arr.Length < 3) {
                    Status<string> status = new Status<string>();
                    status.Code = 100;
                    status.Msg = "用户秘钥错误";
                    status.Data = "";
                    context.Result = new JsonResult(status);
                    return;
                }
              



            }
            base.OnActionExecuting(context);
        }
    }
}
