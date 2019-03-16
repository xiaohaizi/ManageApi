using ManageApi.Common;
using ManageApi.Model.ModelBll;
using ManageApi.Model.ViewModel;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManageApi.Model
{
    [SugarTable("m_admin")]
    public class Admin
    {
        /// <summary>
        /// auto_increment
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// username
        /// </summary>
        public string Username
        {
            get;
            set;
        }
        /// <summary>
        /// userpwd
        /// </summary>
        public string Userpwd
        {
            get;
            set;
        }
        /// <summary>
        /// roleid
        /// </summary>
        public int RoleID
        {
            get;
            set;
        }
        /// <summary>
        /// addtime
        /// </summary>
        public DateTime Addtime
        {
            get;
            set;
        }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>
        public DateTime Logintime
        {
            get;
            set;
        }
        /// <summary>
        /// headimg
        /// </summary>
        public string Headimg
        {
            get;
            set;
        }
        /// <summary>
        /// encrypt
        /// </summary>
        public string Encrypt
        {
            get;
            set;
        }
        public string Guid
        {
            get;
            set;
        }
        
        [SugarColumn(IsIgnore = true)]
        public string Error
        {
            get;
            set;
        }

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <returns></returns>
        public bool Add() {
            bool is_add = true;
           int  count= MysqlDbContext.Instance.Admin.Count(x => x.Username == this.Username);
            if (count > 0) {
                this.Error = "用户名已存在";
              return  false;
            }
            is_add= MysqlDbContext.Instance.Admin.Insert(this);

            return is_add;
        }

        /// <summary>
        /// 
        /// 获取分页数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="_pageIndex"></param>
        /// <returns></returns>
        public PageReturn<AdminRoleViewModel> GetPageList(Expression<Func<Admin, bool>> whereExpression,int _pindex=1) {
            var list = MysqlDbContext.Instance.Db.Queryable<Admin, Role>((a, r) => new object[] {
                JoinType.Left,a.RoleID==r.ID,
            }).Where(whereExpression).Select((a, r) => new AdminRoleViewModel {
                ID = a.ID,
                Username = a.Username,
                Headimg = a.Headimg,
                RoleName = r.Rolename,
                Addtime = a.Addtime,
                RoleID = a.RoleID,
                Logintime = a.Logintime
            });

          
            PageReturn<AdminRoleViewModel> pageReturn = new PageReturn<AdminRoleViewModel>();
            var data=pageReturn.GetPageList(list, _pindex);
            return data;
        }


    }
}
