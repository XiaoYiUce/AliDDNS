using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using AlibabaCloud.SDK.Alidns20150109.Models;

namespace AliDDNS.Utils
{
    public class NetUtils
    {
        /// <summary>
        /// 获取当前PC公网IP地址
        /// </summary>
        /// <returns>HTTP状态码为200时返回当前公网IP，否则返回错误代码</returns>
        public static string getPublicIP()
        {
            HttpClient client = new HttpClient();
            var getIPUri = "https://ip.hiyun.me/";
            HttpResponseMessage response=client.GetAsync(getIPUri).Result;
            if(response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                //HTTP访问状态码异常，返回错误代码
                return "获取IP地址失败，错误码:"+response.StatusCode;
            }
        }
    }
}
