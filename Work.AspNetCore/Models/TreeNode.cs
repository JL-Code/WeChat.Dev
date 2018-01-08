using System.Collections.Generic;

namespace Work.AspNetCore.Models
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNode<T>
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 节点父级ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 节点排序
        /// </summary>
        public long Order { get; set; }

        /// <summary>
        /// 额外属性
        /// </summary>
        public T Attributes { get; set; }

        /// <summary>
        /// 节点子级
        /// </summary>
        public List<TreeNode<T>> Children { get; set; }
    }
}
