using System.Collections.Generic;

namespace WeChat.Dev.Models
{
    public class MenuModel
    {
        public string Name { get; set; } = "";

        public bool IsDisplay { get; set; }

        /// <summary>
        /// 按钮类型 0.button 1.a
        /// </summary>
        public ButtonType ButtonType { get; set; }

        /// <summary>
        /// 代码片段
        /// </summary>
        public string CodeSnippet { get; set; } = "";

        /// <summary>
        /// 跳转URL
        /// </summary>
        public string Url { get; set; } = "";

        /// <summary>
        /// 样式类集合
        /// </summary>
        public List<string> ClassList { get; set; } = new List<string>();

        /// <summary>
        /// 额外的属性 Notation提示数量
        /// </summary>
        public dynamic Extra { get; set; } = new { };

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuModel> SubMenus { get; set; } = new List<MenuModel>();
    }
}