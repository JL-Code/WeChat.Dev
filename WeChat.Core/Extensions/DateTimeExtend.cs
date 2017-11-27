using System;

namespace Zap.WeChat.SDK.Extensions
{
    public static class DateTimeExtend
    {
        /// <summary>
        /// C# DateTime转换为Unix时间戳
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToUnix(this DateTime date)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(DateTime.Now - startTime).TotalSeconds; // 相差秒数
            return timeStamp;
        }

        /// <summary>
        ///  Unix时间戳转换为C# DateTime
        /// </summary>
        /// <param name="unix"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long unix)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddSeconds(unix);
            return dt;
        }
    }
}
