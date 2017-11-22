using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeChat.Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// 添加到仓储
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        T Add(T entity);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid">主键Guid</param>
        /// <returns></returns>
        Task<T> GetAsync(object guid);

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> ListEntities();
    }
}
