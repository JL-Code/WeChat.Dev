using System.Collections.Generic;

namespace WeChat.Dev.Models
{
    public class NavModel
    {
        public List<string> ClassList { get; set; }

        public List<Menu> Menus { get; set; }
    }

    public class Menu
    {
        public string Name { get; set; }

        public bool IsDisplay { get; set; }

        public ButtonType ButtonType { get; set; }

        /// <summary>
        /// 代码片段
        /// </summary>
        public string CodeSnippet { get; set; }

        /// <summary>
        /// 跳转URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 样式类集合
        /// </summary>
        public List<string> ClassList { get; set; }

        /// <summary>
        /// 额外的属性 Notation提示数量
        /// </summary>
        public dynamic Extra { get; set; }
    }

    public enum ButtonType
    {
        /// <summary>
        /// 按钮
        /// </summary>
        Button = 0,

        /// <summary>
        /// a标签
        /// </summary>
        Link = 1
    }
}