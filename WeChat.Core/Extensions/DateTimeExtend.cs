using System;

namespace Zap.WeChat.SDK.Extensions
{
    public static class DateTimeExtend
    {

        static readonly DateTime _startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区

        /// <summary>
        /// C# DateTime转换为Unix时间戳
        /// Unix时间戳(Unix timestamp)，或称Unix时间(Unix time)、POSIX时间(POSIX time)，是一种时间表示方式，定义为从格林威治时间1970年01月01日00时00分00秒(北京时间1970年01月01日08时00分00秒)起至现在的总秒数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToUnix(this DateTime date)
        {
            long timeStamp = (long)(date - _startTime).TotalSeconds; // 相差秒数
            return timeStamp;
        }

        /// <summary>
        ///  Unix时间戳转换为C# DateTime
        ///  Unix时间戳(Unix timestamp)，或称Unix时间(Unix time)、POSIX时间(POSIX time)，是一种时间表示方式，定义为从格林威治时间1970年01月01日00时00分00秒(北京时间1970年01月01日08时00分00秒)起至现在的总秒数
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
        /// JavaScript时间戳：是指格林威治时间1970年01月01日00时00分00秒(北京时间1970年01月01日08时00分00秒)起至现在的总毫秒数。
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
