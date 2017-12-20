using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeChat.Infrastructure
{
    /// <summary>
    /// http请求工具
    /// </summary>
    public class HttpKit : IDisposable
    {
        private readonly HttpClient httpClient;
        private HttpClientHandler httpClientHandler;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="proxyAddrss"></param>
        /// <param name="proxyUserName"></param>
        /// <param name="proxyPassword"></param>
        public HttpKit(Uri proxyAddrss = null, string proxyUserName = null, string proxyPassword = null)
        {
            if (proxyAddrss != null)
            {
                //启用代理
                httpClientHandler = new HttpClientHandler()
                {
                    UseProxy = true,
                    UseCookies = true,
                    UseDefaultCredentials = false,
                };

            }
            else
            {
                httpClientHandler = new HttpClientHandler() { UseCookies = true };
            }
            httpClient = new HttpClient(httpClientHandler)
            {
                Timeout = new TimeSpan(1, 0, 0)
            };

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="baseAddress">父地址</param>
        public HttpKit(string baseAddress)
        {
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            httpClient?.Dispose();
        }

        /// <summary>
        /// 发送一个get请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null)
        {
            T tmodel = default(T);
            httpClient.DefaultRequestHeaders.Host = (new Uri(url).Host);
            if (headers != null)
            {
                foreach (var key in headers.Keys)
                {
                    httpClient.DefaultRequestHeaders.Add(key, headers[key]);
                }
            }
            using (var result = await httpClient.GetAsync(url))
            {
                tmodel = await result.Content.ReadAsAsync<T>();
            }
            return tmodel;
        }

        /// <summary>
        /// 发送一个get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string> headers = null)
        {
            httpClient.DefaultRequestHeaders.Host = (new Uri(url).Host);
            if (headers != null)
            {
                foreach (var key in headers.Keys)
                {
                    httpClient.DefaultRequestHeaders.Add(key, headers[key]);
                }
            }
            var result = await httpClient.GetAsync(url);
            return result;
        }


        /// <summary>
        /// 发送一个异步的post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<string> PostAsync(string url, HttpContent content, Dictionary<string, string> headers = null)
        {
            httpClient.DefaultRequestHeaders.Host = (new Uri(url).Host);
            if (headers != null)
            {
                foreach (var key in headers.Keys)
                {
                    httpClient.DefaultRequestHeaders.Add(key, headers[key]);
                }
            }
            using (var result = await httpClient.PostAsync(url, content))
            {
                return await result.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// 发送一个异步的post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<ResponseData<T>> PostAsync<T>(string url, HttpContent content, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage respData;
            var result = new ResponseData<T>();
            try
            {
                if (headers != null)
                {
                    foreach (var key in headers.Keys)
                    {
                        httpClient.DefaultRequestHeaders.Add(key, headers[key]);
                    }
                }
                using (respData = await httpClient.PostAsync(url, content))
                {
                    result.ReasonPhrase = respData.ReasonPhrase;
                    result.StatusCode = respData.StatusCode;
                    result.Content = await respData.Content.ReadAsAsync<T>();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

    }

    /// <summary>
    /// http 响应数据
    /// </summary>
    /// <typeparam name="TResult">响应正文的类型</typeparam>
    public class ResponseData<TResult>
    {

        /// <summary>
        /// 响应正文
        /// </summary>
        public TResult Content { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public string ReasonPhrase { get; set; }

        /// <summary>
        /// 是否请求成功
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return StatusCode == HttpStatusCode.OK;
            }
        }
        /// <summary>
        /// 响应状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }

}
