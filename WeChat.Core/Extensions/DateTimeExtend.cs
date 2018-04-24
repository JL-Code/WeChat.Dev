using System;

namespace Zap.WeChat.SDK.Extensions
{
    public static class DateTimeExtend
    {
        static readonly DateTime _startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
        /// <summary>
        /// C# DateTime转换为Unix时间戳
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToUnix(this DateTime date)
        {
            long timeStamp = (long)(DateTime.Now - _startTime).TotalSeconds; // 相差秒数
            return timeStamp;
        }

        /// <summary>
        ///  Unix时间戳转换为C# DateTime
        /// </summary>
        /// <param name="unix"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long unix)
        {
            DateTime dt = _startTime.AddSeconds(unix);
            return dt;
        }

        /// <summary>
        /// C#时间转为JS时间戳
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static long ToJSTimeStamp(this DateTime datetime)
        {
            TimeSpan span = datetime - _startTime;
            return (long)span.TotalMilliseconds;
        }
    }
}
