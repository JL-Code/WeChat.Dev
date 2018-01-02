using Senparc.Weixin.Work.AdvancedAPIs.MailList;
using Senparc.Weixin.Work.AdvancedAPIs.Mass;
using System;
using System.Collections.Generic;
using System.Linq;
using Zap.WeChat.SDK.AdvancedAPIs.AddressList;
using Zap.WeChat.SDK.MessageAPI;

namespace Zap.WeChat.SDK
{
    public static class ResultExtends
    {
        public static MessageResult ToMsgResult(this MassResult result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));
            return new MessageResult
            {
                ErrorCode = result.ErrorCodeValue,
                ErrorMessage = result.errmsg,
                InvalidParty = result.invalidparty,
                InvalidTag = result.invalidtag,
                InvalidUser = result.invaliduser
            };
        }

        public static MemberResult ToMemberResult(this GetMemberResult result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));
            return new MemberResult
            {
                ErrorCode = result.ErrorCodeValue,
                ErrorMessage = result.errmsg,
                UserID = result.userid,
                Name = result.name,
                Department = result.department,
                Order = result.order,
                Position = result.position,
                Mobile = result.mobile,
                Gender = result.gender,
                Email = result.email,
                IsLeader = result.isleader,
                Avatar = result.avatar,
                TelePhone = result.telephone,
                EnglishName = result.english_name,
                Extattr = new AdvancedAPIs.AddressList.Extattr
                {
                    Attrs = result.extattr.attrs.Select(m => new AdvancedAPIs.AddressList.Attr
                    {
                        Name = m.name,
                        Value = m.value
                    }).ToList()
                },
                Status = result.status
            };
        }

        /// <summary>
        /// 将SDK中的部门成员详情信息转换为Zap.WeChat.SDK中的部门成员详情信息
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static List<MemberResult> ToDepartmentMemberInfoResult(this GetDepartmentMemberInfoResult result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));
            var list = result.userlist?.Select(user => new MemberResult
            {
                ErrorCode = user.ErrorCodeValue,
                ErrorMessage = user.errmsg,
                UserID = user.userid,
                Name = user.name,
                Department = user.department,
                Order = user.order,
                Position = user.position,
                Mobile = user.mobile,
                Gender = user.gender,
                Email = user.email,
                IsLeader = user.isleader,
                Avatar = user.avatar,
                TelePhone = user.telephone,
                EnglishName = user.english_name,
                Extattr = new AdvancedAPIs.AddressList.Extattr
                {
                    Attrs = user.extattr.attrs.Select(m => new AdvancedAPIs.AddressList.Attr
                    {
                        Name = m.name,
                        Value = m.value
                    }).ToList()
                },
                Status = user.status
            })?.ToList();
            return list;
        }

        public static List<Senparc.Weixin.Work.Entities.Article> ToNews(this NewsBody data)
        {
            return data.Articles.Select(news => new Senparc.Weixin.Work.Entities.Article
            {
                Description = news.Description,
                PicUrl = news.PicUrl,
                Title = news.Title,
                Url = news.Url
            }).ToList();
        }


        public static AdvancedAPIs.AddressList.CreateDepartmentResult ToCreateDepartmentResult(this Senparc.Weixin.Work.AdvancedAPIs.MailList.CreateDepartmentResult result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));
            return new AdvancedAPIs.AddressList.CreateDepartmentResult
            {
                ErrorCode = result.ErrorCodeValue,
                ErrorMessage = result.errmsg,
                Id = result.id
            };
        }


        //public static Dictionary<string, List<MemberResult>> To(this List<MemberResult> result)
        //{
        //    result.ForEach(m =>
        //    {
        //        m.pinyin = PinYinUtil.GetPinyin(m.name);
        //        m.namealif = PinYinUtil.GetFirstPinyin(m.name.Substring(0, 1));
        //        if (!string.IsNullOrEmpty(m.avatar))
        //        {
        //            if (m.avatar.IndexOf("shp.qpic.cn") > 0)
        //            {
        //                m.avatar += "64";
        //            }
        //            else
        //            {
        //                if (minAvatar)
        //                    m.avatar = m.avatar.Replace("/0", "/100");
        //            }
        //        }
        //    });
        //    var newlist = respone.UserList.GroupBy(m => m.namealif).ToDictionary(m => m.Key, m => m.ToList());
        //    return newlist;
        //}
    }
}
