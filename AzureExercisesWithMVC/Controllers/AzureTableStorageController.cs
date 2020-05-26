using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Web.Mvc;

namespace AzureExercisesWithMVC.Controllers
{
    public class AzureTableStorageController : Controller
    {
        // GET: AzureTableStorage
        public ActionResult AzureTableStorage()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=devstoragepraveen;AccountKey=8rpeH3FpHSUpbkI81nrei2XU8NH6KmvbIgImUc/2STHs9I/fqqDOqr6221XaCSy6R2I+g581PbYbScFHCZp/yg==;EndpointSuffix=core.windows.net");
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();

            string tableName = "Customer";
            CloudTable cloudTable = tableClient.GetTableReference(tableName);

            InsertRecordToTable(cloudTable,"C4","customer4");

            //It will retrieve the customerId, Name and Timestamp based on partitionKey and rowKey.
            Customer c = ReadRecordsFromTable(cloudTable, "C2", "customer2");

            ViewBag.Message = "Record Retrieved from Azure Table.";

            return View();
        }

        public void InsertRecordToTable(CloudTable table,string customerId, string Name)
        {
            Customer customerEntity = new Customer();

            customerEntity.PartitionKey = customerId;
            customerEntity.RowKey = Name;

            TableOperation insertRow = TableOperation.Insert(customerEntity);
            table.Execute(insertRow);

            ViewBag.Message = "Record Inserted Successfully to Azure Table.";
        }
        public Customer ReadRecordsFromTable(CloudTable tableName,string partitionKey, string rowKey)
        {
            TableOperation tableOperation = TableOperation.Retrieve<Customer>(partitionKey, rowKey);

            TableResult tableResult = tableName.Execute(tableOperation);

            return tableResult.Result as Customer;
        }
        public class Customer : TableEntity
        {
            public string customerId { get; set; }
            public string Name { get; set; }
        }
    }
}