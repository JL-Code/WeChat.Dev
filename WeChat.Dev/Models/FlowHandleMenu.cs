using System.Collections.Generic;

namespace WeChat.Dev.Models
{
    public class FlowHandleMenu : MenuModel
    {
        public FlowHandleMenu()
        {
        }
        public FlowHandleType HandleType { get; set; }

        public string Suggestion { get; set; } = "";

        public string NodeExtType { get; set; } = "";

        public bool Back { get; set; } = false;
        /// <summary>
        /// 子菜单
        /// </summary>
        public new List<FlowHandleMenu> SubMenus { get; set; } = new List<FlowHandleMenu>();
    }
}