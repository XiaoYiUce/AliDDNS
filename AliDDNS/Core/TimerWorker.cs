using AlibabaCloud.SDK.Alidns20150109.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;
using AliDDNS.Model;

namespace AliDDNS.Core
{
    public class TimerWorker
    {
        
        private Timer timer; 
        public WorkConf workConf { get; private set; }
        private string name => workConf.Name;

        public TimerWorker(WorkConf workConf) {
            this.workConf = workConf;
            timer= new Timer();
            timer.Elapsed += Work;
            timer.Interval = 2000;
        }

        public void Run()
        {
            Console.WriteLine(DateTime.Now.ToString()+ $"[INFO]{name} worker running....");
            timer.Start();
        }

        public void Stop() {
            Console.WriteLine(DateTime.Now.ToString() + $"[INFO]{name} worker stopping....");
            timer.Stop();
        }

        private void Work(object sender, ElapsedEventArgs e)
        {
            if (workConf != null)
            {
                timer.Interval = workConf.Interval * 60 * 1000;
                Console.WriteLine(DateTime.Now + "Update Working....");
                DescribeDomainRecords describeDomainRecords = new DescribeDomainRecords(workConf.accessKeyId, workConf.accessKeySecret
                    , workConf.SubDomainName, workConf.DomainName,workConf.recordType);
                if (!describeDomainRecords.RecordsIsExists())
                {
                    //子域名不存在，新建子域名

                    string ip = "";
                    //判断解析类型为IPv6还是IPv4
                    if (workConf.recordType == "A")
                    {
                        ip = Utils.NetUtils.getPublicIP();
                    }
                    else if(workConf.recordType == "AAAA")
                    {
                        ip= Utils.NetUtils.getIPv6Address();
                    }

                    AddDomainRecord addDomainRecord = new AddDomainRecord(workConf.accessKeyId, workConf.accessKeySecret
                        , workConf.SubDomainName, ip, workConf.DomainName,workConf.recordType);
                    string msg = addDomainRecord.addDomainRecord();
                    Console.WriteLine(msg);
                }
                else
                {
                    //子域名存在，更新子域名值
                    foreach (var record in describeDomainRecords.RRRecords())
                    {
                        if (string.Compare(record.Type, workConf.recordType, StringComparison.OrdinalIgnoreCase) == 0 &&
                            string.Compare(record.RR, workConf.SubDomainName, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            //成功匹配
                            string RecordId = record.RecordId;
                            UpdateDomainRecord updateDomainRecord = new UpdateDomainRecord(workConf.accessKeyId, workConf.accessKeySecret,
                                workConf.SubDomainName, workConf.DomainName, RecordId, Utils.NetUtils.getPublicIP());
                            if (updateDomainRecord.updateDomainRecord())
                            {
                                Console.WriteLine(DateTime.Now.ToString() + "[Info]Update Success");
                            }
                        }
                    }
                }

            }
        }
    }
}
