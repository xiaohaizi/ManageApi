using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ManageApi.Common;
using ManageApi.Model;
using ManageApi.Filter;
using ManageApi.Base;
using ManageApi.Model.ModelBll;

namespace ManageApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : CommonController
    {
        // GET api/values
        [HttpGet]       
        public JsonResult Get()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var list = MysqlDbContext.Instance.Menu.GetList().Select(c => new ReturnMenu
            {
                ID = c.ID,
                Title = c.Title,
                Pid = c.Pid,
                Level = c.Level,
                Url = c.Url
            }).ToList();
            var list_0=   Business.RecursionMenu(list);

            dic.Add("Test", "Test");
            return Json(list);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("getmodel")]
        public IActionResult GetModel()
        {
            var list = MysqlDbContext.Instance.Menu.GetList();
            return Json(list);
        }
    }
}
