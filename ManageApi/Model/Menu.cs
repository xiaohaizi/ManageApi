using ManageApi.Common;
using ManageApi.Model.ModelBll;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageApi.Model
{
    [SugarTable("m_menu")]
    public class Menu
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        public string Title { get; set; }

        public int Level { get; set; }


        public int Pid { get; set; }

        public string Url { get; set; }
        public DateTime Addtime { get; set; }

        public DateTime Update_time { get; set; }
        public string Path { get; set; }
        public int Mtype { get; set; }

        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<ReturnMenu> UserMenu(int user_id)
        {
            List<ReturnMenu> menus = new List<ReturnMenu>();
            var user = MysqlDbContext.Instance.Admin.GetById(user_id);
            if (user != null)
            {
                var role_menu = MysqlDbContext.Instance.RoleMenu.GetList(x => x.Role_id == user.RoleID).ToList();
                int count = role_menu.Count;
              
                int[] menu_ids =new  int [count] ;
                for (int n=0;n< role_menu.Count;n++) {                    
                    menu_ids[n] = role_menu[n].Menu_id;
                }               
                if (menu_ids.Length > 0) {
                  var   user_menus= MysqlDbContext.Instance.Db.Queryable<Menu>().In(m => m.ID, menu_ids)
                        .Select((m)=>new ReturnMenu {
                            ID=m.ID,
                            Level=m.Level,
                            Title=m.Title,
                            Url=m.Url,
                            Pid=m.Pid                            
                        }).ToList();
                    menus= Business.RecursionMenu(user_menus);
                }
            }
            return menus;
        }

    }
}
