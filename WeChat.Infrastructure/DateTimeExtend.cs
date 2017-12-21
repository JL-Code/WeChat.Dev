using System;

namespace WeChat.Infrastructure
{
    public static class DateTimeExtend
    {
        private readonly static DateTime _startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区

        /// <summary>
        ///  C# DateTime转换为JavaScript时间戳
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static long ToJSTimeStamp(this DateTime datetime)
        {
            long timeStamp = (long)(datetime - _startTime).TotalMilliseconds; // 相差毫秒数
            return timeStamp;
        }

        /// <summary>
        ///  JavaScript时间戳转换为C# DateTime
        /// </summary>
        /// <param name="jsTimeStamp ">JavaScript时间戳</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long jsTimeStamp)
        {
            DateTime dt = _startTime.AddMilliseconds(jsTimeStamp);
            return dt;
        }

        /// <summary>
        /// C# DateTime转换为Unix时间戳
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static long ToUnix(this DateTime datetime)
        {
            long timeStamp = (long)(datetime - _startTime).TotalSeconds; // 相差秒数
            return timeStamp;
        }

        /// <summary>
        /// Unix时间戳转换为C# DateTime
        /// </summary>
        /// <param name="unixTimeStamp">Unix时间戳</param>
        /// <returns></returns>
        public static DateTime UnixToDateTime(this long unixTimeStamp)
        {
            DateTime dt = _startTime.AddSeconds(unixTimeStamp);
            return dt;
        }
    }
}
