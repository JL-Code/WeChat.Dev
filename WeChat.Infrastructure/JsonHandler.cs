using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WeChat.Infrastructure
{
    public class JsonHandler
    {
        /// <summary> 
        /// 对象转JSON 
        /// </summary> 
        /// <param name="obj">对象</param> 
        /// <param name="dateTimeFormat">时间序列化格式</param>
        /// <returns>JSON格式的字符串</returns> 
        public static string ToJson(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj, Formatting.Indented);
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.ObjectToJSON(): " + ex.Message);
            }
        }

        /// <summary> 
        /// JSON文本转对象,泛型方法 
        /// </summary> 
        /// <typeparam name="T">类型</typeparam> 
        /// <param name="jsonText">JSON文本</param> 
        /// <returns>指定类型的对象</returns> 
        public static T ToObject<T>(string jsonText)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonText);
            }
            catch (Exception ex)
            {
                throw new Exception("JsonHelper.JSONToObject(): " + ex.Message);
            }
        }

        /// <summary>
        /// JSON文本转对象集合,泛型方法 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static List<T> ToObjects<T>(string jsonText)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(jsonText);
            }
            catch (Exception ex)
            {
                throw new Exception("JsonHelper.JSONToObject(): " + ex.Message);
            }
        }

    }
}
