using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Net;
using DataCollector.Local.PC.DAL;
using Newtonsoft.Json;

namespace DataCollector.Local.PC.Services
{
    public interface ISender
    {
        void SendRecords(ICollection<DiskStateRecord> records);
    }

    class Sender : ISender
    {
        private readonly MySettings _settings;

        public Sender(MySettings settings)
        {
            _settings = settings;
        }

        public void SendRecords(ICollection<DiskStateRecord> records)
        {
            var content = GetStringFromRecords(records);
            var cli = new WebClient
            {
                Headers = {[HttpRequestHeader.ContentType] = "application/json"}
            };
            string response = cli.UploadString(_settings.ServerUrl, content);
        }

        private string GetStringFromRecords(ICollection<DiskStateRecord> records)
        {
            return JsonConvert.SerializeObject(records);
        }
    }
}