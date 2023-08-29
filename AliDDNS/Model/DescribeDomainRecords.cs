using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Tea;
using AlibabaCloud.SDK.Alidns20150109;
using AlibabaCloud.SDK;
using AlibabaCloud.SDK.Alidns20150109.Models;
using Tea.Utils;
using System.Diagnostics;
using System.Collections;

namespace AliDDNS.Model
{
    public class DescribeDomainRecords : DomainRecord
    {
        /// <summary>
        /// 查询对应子域名是否存在
        /// </summary>
        /// <param name="accessKeyId">阿里云accessKeyId</param>
        /// <param name="accessKeySecret">阿里云accessKeySecret</param>
        /// <param name="RR">子域名前缀</param>
        /// <param name="domain">要查询的域名</param>
        public DescribeDomainRecords(string accessKeyId, string accessKeySecret, string RR, string domain) :
            base(accessKeyId, accessKeySecret, RR, domain)
        {
        }

        /// <summary>
        /// 查询RR记录是否存在
        /// </summary>
        /// <returns>如存在，返回true，否则为false</returns>
        public bool RecordsIsExists()
        {
            //创建client对象
            Client client = new Client(config);
            //创建查询对象
            DescribeDomainRecordsRequest request =
                new DescribeDomainRecordsRequest
                {
                    DomainName = this.domain,
                    RRKeyWord = this.RR
                };
            //执行查询
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                DescribeDomainRecordsResponse response = client.DescribeDomainRecordsWithOptions(request, runtime);
                if (response.StatusCode == 200)
                {
                    long? Count = response.Body.TotalCount.Value;
                    Debug.WriteLine(Count);
                    if (Count > 0)
                    {
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("调用阿里云API失败，错误码:" + response.StatusCode);
                }

            }
            catch (TeaException error)
            {
                Console.WriteLine(error.Message.ToString());
            }

            return false;
        }

        /// <summary>
        /// 以关键词查询子域名记录
        /// </summary>
        /// <returns>子域名前缀，如果没有相关子域名记录数组Count=0</returns>
        public List<RRRecord> RRRecords()
        {
            List<RRRecord> records = new List<RRRecord>();
            //创建client对象
            Client client = new Client(config);
            //创建查询对象
            DescribeDomainRecordsRequest request =
                new DescribeDomainRecordsRequest
                {
                    DomainName = this.domain,
                    RRKeyWord = this.RR
                };
            //执行查询
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                DescribeDomainRecordsResponse response = client.DescribeDomainRecordsWithOptions(request, runtime);
                if (response.StatusCode == 200)
                {
                    //HTTP状态码正常
                    long? Count = response.Body.TotalCount;
                    Debug.WriteLine(Count);
                    if (Count > 0)
                    {
                        //找到RR关键词相关记录大于0
                        for (int i = 0; i < Count; i++)
                        {
                            RRRecord record = new RRRecord();
                            record.RecordId = response.Body.DomainRecords.Record[i].RecordId;
                            record.RR = response.Body.DomainRecords.Record[i].RR;
                            record.Type = response.Body.DomainRecords.Record[i].Type;
                            records.Add(record);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("调用阿里云API失败，错误码:" + response.StatusCode);
                }

            }
            catch (TeaException error)
            {
                Console.WriteLine(error.Message.ToString());
            }

            return records;
        }
    }

    public class RRRecord
    {
        public string? RR { get; set; }
        public string? RecordId { get; set; }
        public string? Type { get; set; }
    }
}
