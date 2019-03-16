using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageApi.Model
{
    [SugarTable("m_role_menu")]
    public class RoleMenu
    {
        /// <summary>
        /// auto_increment
        /// </summary>		
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID
        {
            get; set;
        }
        /// <summary>
        /// role_id
        /// </summary>		

        public int Role_id
        {
            get; set;
        }
        /// <summary>
        /// menu_id
        /// </summary>		

        public int Menu_id
        {
            get; set;
        }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime Add_time
        {
            get; set;
        }

    }
}
