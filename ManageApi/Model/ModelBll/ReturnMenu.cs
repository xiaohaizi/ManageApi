using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageApi.Model.ModelBll
{
    /// <summary>
    /// 
    /// API 返回菜单信息
    /// </summary>
    public class ReturnMenu
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int Level { get; set; }

        public int Pid { get; set; }

        public string Url { get; set; }
      
        public List<ReturnMenu> Child = new List<ReturnMenu>();

       
    }
}
