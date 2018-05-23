using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureLearning.Models
{
    public class Employee : TableEntity
    {
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public int MyProperty { get; set; }

        
        public static CloudTable GetTable()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var client = storageAccount.CreateCloudTableClient();
            return client.GetTableReference("Employee");
        }
    }
}