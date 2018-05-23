using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AzureLearning.Controllers
{
    public class AzureBlobController : Controller
    {
        // GET: AzureBlob
        public ActionResult Index()
        {
            var blobContainer = GetCloudBlobContainer();
            var success = blobContainer.CreateIfNotExists();
            ViewBag.BlobContainerName = blobContainer.Name;
            var blockBlob = blobContainer.GetBlockBlobReference("secondBlock");
            using (var fileStream = System.IO.File.OpenRead(@"D:\file.txt"))
            {
                blockBlob.UploadFromStream(fileStream);
            }
            //List all blobs in the container reference
            List<string> blobsAvailable = new List<string>();
            foreach (var blobItem in blobContainer.ListBlobs(useFlatBlobListing:true).Select(b => b.Uri))
            {
                //if(blobItem.GetType() == typeof(CloudBlockBlob))
                //{
                //    CloudBlockBlob blob = (CloudBlockBlob)blobItem;
                //    blobsAvailable.Add(blob.u);
                //}
                blobsAvailable.Add(blobItem.AbsoluteUri);
            }
            // DownloadBlob
            var firstBlockBlob = blobContainer.GetBlobReference("firstBlock");
            using (var fileStream = System.IO.File.OpenWrite(@"D:\firstBlckDownloadFile.png"))
            {
                firstBlockBlob.DownloadToStream(fileStream);
            }
            // Delete Blob
            //firstBlockBlob.Delete();
            return View(blobsAvailable);
        }

        private CloudBlobContainer GetCloudBlobContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("umasstorage_AzureStorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("uma-blobcontainer");
            return container;
        }
    }
}