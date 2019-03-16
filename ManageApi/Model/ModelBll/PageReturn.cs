using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageApi.Model.ModelBll
{
    /// <summary>
    /// 返回List集合数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageReturn<T> where T: class,new()
    {
        public List<T> list { get; set; }
        public PageModel page { get; set; }
        /// <summary>
        /// 数据分页
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public PageReturn<T> GetPageList(ISugarQueryable<T>list,int page_index=1) {
            PageModel page = new PageModel();
            page.PageCount = list.Count();
            page.PageSize = int.Parse(Math.Ceiling(double.Parse(page.PageCount.ToString()) / double.Parse("10.00")).ToString());
            List<T> page_data = list.Skip((page_index - 1) * 10).Take(10).ToList();
            PageReturn<T> pageReturn = new PageReturn<T>();
            pageReturn.list = page_data;
            page.PageIndex = page_index;
            pageReturn.page = page;
            return pageReturn;
        }
        
    }
}
