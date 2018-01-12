using System.Collections.Generic;

namespace WeChat.Dev.Models
{
    public class NavModel
    {

        public List<string> ClassList { get; set; } = new List<string>();

        public List<MenuModel> Menus { get; set; } = new List<MenuModel>();

        public List<GroupModel> Groups { get; set; } = new List<GroupModel>();
    }

    public class NavModel<TMenu> : NavModel where TMenu : MenuModel
    {
        public new List<TMenu> Menus { get; set; } = new List<TMenu>();
    }

    public class GroupModel
    {
        public string Name { get; set; }

        public List<MenuModel> Items { get; set; }
    }

    public enum ButtonType
    {
        /// <summary>
        /// a标签
        /// </summary>
        Link = 0,

        /// <summary>
        /// 按钮
        /// </summary>
        Button = 1
    }
}