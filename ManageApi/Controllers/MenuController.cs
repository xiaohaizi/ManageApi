using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManageApi.Base;
using ManageApi.Common;
using ManageApi.Model;
using ManageApi.Model.ModelBll;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace ManageApi.Controllers
{

    public class MenuController : CommonController
    {
        [HttpGet]
        public IActionResult Get(int p=1)
        {
            PageModel page = new PageModel();
            page.PageIndex = p;
            page.PageSize = 10;
            PageReturn<Menu> pageReturn = new PageReturn<Menu>();

            Status<PageReturn<Menu>> status = new Status<PageReturn<Menu>>();
            var list = MysqlDbContext.Instance.Menu.GetPageList(a => a.ID > 0, page);

            
            var page_list=list.ToList();
            pageReturn.list = page_list;
          
           
            status.Code = 200;
            status.Data = pageReturn;
            return Json(status);
        }


        [HttpPost("addmenu")]
        public IActionResult AddMenu()
        {
            Status<string> status = new Status<string>();
            status.Code = 100;
            string title = Request.Form["title"];
            if (string.IsNullOrWhiteSpace(title))
            {
                status.Msg = "菜单名称不能为空";
                return Json(status);
            }
            int level = 0;
            int pid = 0;
            int.TryParse(Request.Form["pid"], out pid);
            string url = Request.Form["url"];
            if (string.IsNullOrWhiteSpace(url))
            {
                status.Msg = "菜单地址不能为空";
                return Json(status);
            }
            int mtype = 0;
            int.TryParse(Request.Form["mtype"], out mtype);
            if (mtype < 1)
            {
                status.Msg = "请选择菜单类型";
                return Json(status);
            }
            DateTime addtime = DateTime.Now;
            DateTime update_time = DateTime.Now;
            string path = "";
            if (pid > 0)
            {
                var p_menu = MysqlDbContext.Instance.Menu.GetById(pid);

                if (p_menu != null)
                {
                    level = p_menu.Level + 1;
                    path = "," + p_menu.Path.Trim(',') + "," + p_menu.ID.ToString() + ",";
                }
            }
            Menu menu = new Menu();
            menu.Level = level;
            menu.Mtype = mtype;
            menu.Path = path;
            menu.Pid = pid;
            menu.Title = title;
            menu.Update_time = update_time;
            menu.Addtime = addtime;
            menu.Url = url;
            status.Msg = "菜单添加失败";
            if (MysqlDbContext.Instance.Menu.Insert(menu))
            {
                status.Code = 200;
                status.Msg = "菜单添加成功";
            }
            return Json(status);
        }

        [HttpGet("usernume")]
        public IActionResult UserMenu() {
            int user_id = 2;
            Menu menu = new Menu();
            Status<List<ReturnMenu>> status = new Status<List<ReturnMenu>>();
            status.Code = 200;
            var list=  menu.UserMenu(user_id);
            status.Data = list;
            return Json(status);
        }
    }
}