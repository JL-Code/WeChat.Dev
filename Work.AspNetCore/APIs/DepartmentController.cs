using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.MailList;

namespace Work.AspNetCore.APIs
{
    /// <summary>
    /// 部门
    /// </summary>
    [Produces("application/json")]
    [Route("api/Department")]
    public class DepartmentController : ApiController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DepartmentController()
        {

        }

        #region APIs

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public GetDepartmentListResult Get()
        {
            var result = MailListApi.GetDepartmentList(APP_KEY);
            return result;
        }

        /// <summary>
        /// 创建部门
        /// 系统应用须拥有父部门的管理权限。
        /// 注意，部门的最大层级为15层；部门总数不能超过3万个；每个部门下的节点不能超过3万个。建议保证创建的部门和对应部门成员是串行化处理。
        /// </summary>
        /// <param name="name">部门名称。长度限制为1~64个字节，字符不能包括\:?”<>｜</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <returns></returns>
        [HttpPost]
        public CreateDepartmentResult CreateDepartment(string name, long parentId)
        {
            var result = MailListApi.CreateDepartment(APP_KEY, name, parentId);
            return result;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        [HttpDelete]
        public WorkJsonResult Delete(long id)
        {
            var result = MailListApi.DeleteDepartment(APP_KEY, id);
            return result;
        }

        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="name">部门名称</param>
        /// <param name="parentId">父级部门ID</param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后</param>
        /// <returns></returns>
        [HttpPut]
        public WorkJsonResult Update(long id, string name, long parentId, int order)
        {
            var result = MailListApi.UpdateDepartment(APP_KEY, id, name, parentId, order);
            return result;
        }
        #endregion

    }
}