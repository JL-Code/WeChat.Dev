using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zap.WeChat.SDK.AdvancedAPIs.AddressList
{
    public class AddressListResult : MemberResult
    {
        /// <summary>
        /// 名字拼音首字母
        /// </summary>
        [JsonProperty("namealif")]
        public string Namealif { get; set; }

        /// <summary>
        /// 汉字拼音
        /// </summary>
        [JsonProperty("pinyin")]
        public string PinYin { get; set; }
    }
}
