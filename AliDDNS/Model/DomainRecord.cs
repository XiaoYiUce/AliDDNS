using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliDDNS.Model
{
    public class DomainRecord
    {
        private const string Endpoint = "alidns.cn-beijing.aliyuncs.com";
        public string accessKeyId { get;private set; }
        public string accessKeySecret { get;private set; }
        /// <summary>
        /// 解析记录类型，默认值为A
        /// </summary>
        public string recordType { get; set; } = "A";
        public string RR { get; set; }
        public string ?value { get; set; }

        /// <summary>
        /// TTL优先值 默认600秒（10分钟）
        /// </summary>
        public long TTL { get; set; } = 600;
        public string domain { get; set; }


        //初始化账号client
        public AlibabaCloud.OpenApiClient.Models.Config config { get; private set; } = 
            new AlibabaCloud.OpenApiClient.Models.Config();

        /// <summary>
        /// 设置域名解析记录，recordType为A
        /// </summary>
        /// <param name="accessKeyId">阿里云accessKeyId</param>
        /// <param name="accessKeySecret">阿里云accessKeySecret</param>
        /// <param name="RR">子域名前缀</param>
        /// <param name="value">子域名记录值/param>
        public DomainRecord(string accessKeyId, string accessKeySecret,string RR,string value,string domain)
        {
            this.accessKeyId = accessKeyId;
            this.accessKeySecret = accessKeySecret;
            this.RR = RR;
            this.value = value;
            this.domain = domain;
            config.AccessKeyId = accessKeyId;
            config.AccessKeySecret = accessKeySecret;
            config.Endpoint = Endpoint;
        }

        public DomainRecord(string accessKeyId, string accessKeySecret, string RR, string value, string domain,string recordType)
        {
            this.accessKeyId = accessKeyId;
            this.accessKeySecret = accessKeySecret;
            this.RR = RR;
            this.value = value;
            this.domain = domain;
            this.recordType = recordType;
            config.AccessKeyId = accessKeyId;
            config.AccessKeySecret = accessKeySecret;
            config.Endpoint = Endpoint;
        }

        public DomainRecord(string accessKeyId, string accessKeySecret, string RR,string domain)
        {
            this.accessKeyId = accessKeyId;
            this.accessKeySecret = accessKeySecret;
            this.RR = RR;
            this.domain = domain;
            config.AccessKeyId = accessKeyId;
            config.AccessKeySecret = accessKeySecret;
            config.Endpoint = Endpoint;
        }
    }
}

