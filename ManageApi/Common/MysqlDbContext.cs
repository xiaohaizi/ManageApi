using ManageApi.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageApi.Common
{
    public class MysqlDbContext
    {

        public SqlSugarClient Db;
      
        public MysqlDbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = Config.ConnectionString,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value));
                
            };
        }

        public static MysqlDbContext Instance //注意当前方法的类不能是静态的 public static class这么写是错误的
        {
            get { return new MysqlDbContext();}
        }



        public SimpleClient<Menu> Menu { get { return new SimpleClient<Menu>(Db); } }
        public SimpleClient<Users> Users { get { return new SimpleClient<Users>(Db); } }
        public SimpleClient<Admin> Admin { get { return new SimpleClient<Admin>(Db); } }
        public SimpleClient<Role> Role { get { return new SimpleClient<Role>(Db); } }
        public SimpleClient<RoleMenu> RoleMenu { get { return new SimpleClient<RoleMenu>(Db); } }



    }
}
