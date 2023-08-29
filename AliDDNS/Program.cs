using System;
using System.Collections;
using AliDDNS.Core;
using AliDDNS.Model;
using Newtonsoft.Json;

namespace AliDDNS
{
    static class Program
    {
        public static void Main(string[] args)
        {
            WorkConf workConf = new WorkConf();
            string confDir = Path.Combine(Directory.GetCurrentDirectory(), "conf.json");
            workConf.LoadFromFile(confDir);
            TimerWorker timer = new TimerWorker(workConf);
            Thread thread = new Thread(timer.Run);
            thread.Start();
            Console.ReadKey();
        }
    }
}