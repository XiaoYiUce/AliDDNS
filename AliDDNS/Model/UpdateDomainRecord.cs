using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlibabaCloud.SDK.Alidns20150109;
using AlibabaCloud.SDK.Alidns20150109.Models;
using Tea;
using Tea.Utils;

namespace AliDDNS.Model
{
    public class UpdateDomainRecord:DomainRecord
    {
        /// <summary>
        /// 记录值ID
        /// </summary>
        private string RecordId;

        public UpdateDomainRecord(string accessKeyId, string accessKeySecret, string RR, string domain,string RecordId,string Value)
            :base(accessKeyId, accessKeySecret, RR, Value,domain)
        {
            this.RecordId = RecordId;

        }

        public UpdateDomainRecord(string accessKeyId, string accessKeySecret, string RR, string domain, string RecordId, string Value,string recordType)
            : base(accessKeyId, accessKeySecret, RR, Value, domain,recordType)
        {
            this.RecordId = RecordId;

        }

        public bool updateDomainRecord()
        {
            Client client = new Client(config);
            //设置更新记录参数
            UpdateDomainRecordRequest updateDomainRecordRequest = new UpdateDomainRecordRequest
            {
                RecordId = this.RecordId,
                RR = this.RR,
                Type = this.recordType,
                Value = this.value
            };

            //开始更新
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                UpdateDomainRecordResponse updateDomainRecordResponse=client.UpdateDomainRecordWithOptions(updateDomainRecordRequest, runtime);
                if (updateDomainRecordResponse.StatusCode == 200)
                {
                    Console.WriteLine(updateDomainRecordResponse.Body.ToSafeString());
                    return true;
                }
                else
                {
                    Console.WriteLine("调用阿里云API失败，错误码:" + updateDomainRecordResponse.StatusCode);
                    return false;
                }
            }
            catch (TeaException error)
            {
                Console.WriteLine(error.Message + "\n" + error.StackTrace);
            }
            catch (Exception _error)
            {
                TeaException error = new TeaException(new Dictionary<string, object>
                {
                    { "message", _error.Message }
                });

                AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
            }
            return true;
        }
    }
}
