using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work.AspNetCore.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDepartmentModel
    {
        /// <summary>
        /// 部门名称。长度限制为1~64个字节，字符不能包括\:?” ＜＞'｜
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父亲部门id。根部门id为1
        /// </summary>
        public long ParentId { get; set; }
    }
}
