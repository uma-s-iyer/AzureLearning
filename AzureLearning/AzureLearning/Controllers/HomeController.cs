using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using AzureLearning.Models;

namespace AzureLearning.Controllers
{
    public class HomeController : Controller
    {
        CloudStorageAccount storageAccount;
        public HomeController()
        {
            storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        }
        public ActionResult Index()
        {
            // Create or Update EMployee 
            CreateEmployee();
            DeleteEmployee("1");
            var employees = GetEmployees();
            ViewBag.EmployeeNames = employees;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region STORAGE_TABLE_CRUD_OPERATIONS
        private void CreateEmployee()
        {
            var empTable = Employee.GetTable();
            var empToAdd = new Employee() { FirstName = "Harry", LastName = "Jones", PartitionKey = "Employee", RowKey = "2" };
            var operation = TableOperation.InsertOrReplace(empToAdd);
            empTable.Execute(operation);

        }

        private string GetEmployees()
        {
            var empTable = Employee.GetTable();
            var query = empTable.CreateQuery<Employee>().Where(emp => emp.PartitionKey == "Employee").ToList();
            return String.Join(",", query.Select(e => e.FirstName));
        }

        private void DeleteEmployee(string id)
        {
            var empTable = Employee.GetTable();
            var employee = empTable.CreateQuery<Employee>().FirstOrDefault(emp => emp.RowKey == id);
            if(employee != null)
            {
                var delete = TableOperation.Delete(employee);
                empTable.Execute(delete);
            }

        }
        #endregion
    }
}