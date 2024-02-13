using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;

namespace AliDDNS.Core
{
    public class WorkConf
    {
        [JsonProperty]
        public string? accessKeyId { get; set; }
        [JsonProperty]
        public string? accessKeySecret { get; set; }
        [JsonProperty]
        public int Interval { get; set; }
        [JsonProperty]
        public string? DomainName { get; set; }
        [JsonProperty]
        public string? SubDomainName { get; set; }
        [JsonIgnore]
        public string Name => $"{SubDomainName}.{DomainName}";
        [JsonProperty]
        public string? recordType {  get; set; }

        /// <summary>
        /// 从文件导入配置
        /// </summary>
        /// <param name="confFile">配置文件地址</param>
        /// <returns></returns>
        public bool LoadFromFile(string confFile)
        {
            try
            {
                string content = File.ReadAllText(confFile);
                if (content != null)
                {
                    WorkConf? workConf = JsonConvert.DeserializeObject<WorkConf>(content);
                    this.accessKeyId = workConf.accessKeyId;
                    this.accessKeySecret = workConf.accessKeySecret;
                    this.Interval = workConf.Interval;
                    this.DomainName = workConf.DomainName;
                    this.SubDomainName = workConf.SubDomainName;
                    this.recordType = workConf.recordType;
                    return true;
                }
                else
                {
                    return false;
                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
