using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tea;
using Tea.Utils;
using AlibabaCloud.SDK;

namespace AliDDNS.Model
{
    /**
     * 添加域名解析记录
     * 
     */
    public class AddDomainRecord:DomainRecord
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessKeyId">阿里云accessKeyId</param>
        /// <param name="accessKeySecret">阿里云accessKeySecret</param>
        /// <param name="RR">子域名</param>
        /// <param name="value">记录值</param>
        /// <param name="domain">要修改的域名</param>
        public AddDomainRecord(string accessKeyId, string accessKeySecret,string RR, string value,string domain)
            :base(accessKeyId,accessKeySecret,RR,value,domain) {
        }

        public AddDomainRecord(string accessKeyId, string accessKeySecret, string RR, string value, string domain,string recordType)
            : base(accessKeyId, accessKeySecret, RR, value, domain,recordType)
        {
        }

        public string addDomainRecord()
        {
            string returnMsg = "";

            AlibabaCloud.SDK.Alidns20150109.Client client = new AlibabaCloud.SDK.Alidns20150109.Client(config);

            //添加域名解析记录
            AlibabaCloud.SDK.Alidns20150109.Models.AddDomainRecordRequest addDomainRecordRequest = new
                AlibabaCloud.SDK.Alidns20150109.Models.AddDomainRecordRequest();
            addDomainRecordRequest.TTL = TTL;
            addDomainRecordRequest.DomainName = domain;
            addDomainRecordRequest.Value = value;
            addDomainRecordRequest.RR = RR;
            addDomainRecordRequest.Type = this.recordType;

            //执行添加操作
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                AlibabaCloud.SDK.Alidns20150109.Models.AddDomainRecordResponse response =
                client.AddDomainRecordWithOptions(addDomainRecordRequest, runtime);
                if (response.StatusCode == 200)
                {
                    //HTTP返回状态码200
                    if(response.Body == null)
                    {
                        //操作成功执行
                        returnMsg = "OK";
                    }
                    else
                    {
                        //操作失败，返回错误信息
                        returnMsg = response.Body.ToSafeString();
                    }
                }
                else
                {

                }

            }
            catch (TeaException error)
            {
                Console.WriteLine(error.Message.ToString());
            }

            return returnMsg;
        }
    }
}
