using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageApi.Model
{
    [SugarTable("m_role")]
    public class Role
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
        /// rolename
        /// </summary>		

        public string Rolename
        {
            get; set;
        }
        /// <summary>
        /// pid
        /// </summary>		

        public int Pid
        {
            get; set;
        }
        /// <summary>
        /// level
        /// </summary>		

        public int Level
        {
            get; set;
        }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		

        public DateTime Addtime
        {
            get; set;
        }
        /// <summary>
        /// path
        /// </summary>		

        public string Path
        {
            get; set;
        }
    }
}
