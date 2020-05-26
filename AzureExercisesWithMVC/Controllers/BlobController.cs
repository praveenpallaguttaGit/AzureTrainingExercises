using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Web.Mvc;


namespace AzureExercisesWithMVC.Controllers
{
    public class BlobController : Controller
    {
        // GET: Blob
        public ActionResult AzureBlobConnect()
        {
            ViewBag.Message = "Deployed Sample Web App in Azure.";
            
            upload_ToAzureBlob("D:\\SampleAzureBlobFile.txt", "blobfilescontainer");
            download_FromAzureBlob("SampleAzureBlobFile.txt", "blobfilescontainer");

            return View();
          
        }
        public void upload_ToAzureBlob(string fileToUpload, string azure_ContainerName)
        {
            string file_extension, filename_withExtension, storageAccount_connectionString;
            Stream file;

            storageAccount_connectionString = "DefaultEndpointsProtocol=https;AccountName=blobstordev;AccountKey=oCg+GIBRidmRb5g72Nj+RApvY/ls9BhCNQ5fB6YnSlKYCQAdd3CpRBxP9Gp/6xIKG91hlCdp2MNBGDlnpeieZA==;EndpointSuffix=core.windows.net";

            file = new FileStream(fileToUpload, FileMode.Open);

            CloudStorageAccount mycloudStorageAccount = CloudStorageAccount.Parse(storageAccount_connectionString);
            CloudBlobClient blobClient = mycloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(azure_ContainerName);

            //checking the container exists or not  
            if (container.CreateIfNotExists())
            {
                container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            //reading file name & file extention    
            file_extension = Path.GetExtension(fileToUpload);
            filename_withExtension = Path.GetFileName(fileToUpload);

            CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(filename_withExtension);
            cloudBlockBlob.Properties.ContentType = file_extension;
            cloudBlockBlob.UploadFromStreamAsync(file); // << Uploading the file to the blob >>  

            ViewBag.Message = "File Uploaded to Azure Blob.";
        }
        public void download_FromAzureBlob(string filetoDownload, string azure_ContainerName)
        {
            
            string storageAccount_connectionString = "DefaultEndpointsProtocol=https;AccountName=blobstordev;AccountKey=oCg+GIBRidmRb5g72Nj+RApvY/ls9BhCNQ5fB6YnSlKYCQAdd3CpRBxP9Gp/6xIKG91hlCdp2MNBGDlnpeieZA==;EndpointSuffix=core.windows.net";

            CloudStorageAccount mycloudStorageAccount = CloudStorageAccount.Parse(storageAccount_connectionString);
            CloudBlobClient blobClient = mycloudStorageAccount.CreateCloudBlobClient();
            
            CloudBlobContainer container = blobClient.GetContainerReference(azure_ContainerName);
            CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(filetoDownload);

            // provide the file download location below            
            Stream file = System.IO.File.OpenWrite(@"D:\" + filetoDownload);
            cloudBlockBlob.DownloadToStream(file);

            ViewBag.Message = "File Downloaded from Azure Blob.";
        }
    }
}