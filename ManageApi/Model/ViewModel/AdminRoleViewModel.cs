using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageApi.Model.ViewModel
{
    public class AdminRoleViewModel
    {
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

        public string RoleName { get; set; }
    }
}
