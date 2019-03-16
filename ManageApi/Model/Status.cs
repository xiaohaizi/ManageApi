using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageApi.Model
{
    /// <summary>
    /// 状态返回信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Status<T>
    {
        public int Code;
        public string Msg = "success";
        public string Url = "";
        public T Data;
    }
}
