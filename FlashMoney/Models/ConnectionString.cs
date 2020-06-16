using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public static class ConnectionString
    {
        //static string account = CloudConfigurationManager.GetSetting("StorageAccountName");
        //static string key = CloudConfigurationManager.GetSetting("StorageAccountKey");
        public static CloudStorageAccount GetConnectionString()
        {
            string connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName=cyhermes2;AccountKey=urcfobVF7v/3vIOYPUSLBxenz3LmB+n5MAnh+7geauehf2FrT2Wn4nLCC4wLcZMpFX9NpD6ZAG20b7XaylkyTg==;EndpointSuffix=core.windows.net");
            return CloudStorageAccount.Parse(connectionString);
        }
    }
}
