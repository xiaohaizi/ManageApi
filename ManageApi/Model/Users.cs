using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageApi.Model
{
    [SugarTable("m_users")]
    public class Users
    {
        /// <summary>
		/// id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// user_name
        /// </summary>
        public string User_name
        {
            get;
            set;
        }
        /// <summary>
        /// user_pwd
        /// </summary>
        public string User_pwd
        {
            get;
            set;
        }
        /// <summary>
        /// utype
        /// </summary>
        public int Utype
        {
            get;
            set;
        }
        /// <summary>
        /// nickname
        /// </summary>
        public string Nickname
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
        /// login_time
        /// </summary>
        public DateTime Login_time
        {
            get;
            set;
        }
        /// <summary>
        /// update_time
        /// </summary>
        public DateTime Update_time
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
        /// <summary>
        /// user_guid
        /// </summary>
        public string User_guid
        {
            get;
            set;
        }

        public DateTime Register_time
        {
            get;
            set;
        }


        public string Mobile
        {
            get;
            set;
        }


        public string Email
        {
            get;
            set;
        }

    }
}
