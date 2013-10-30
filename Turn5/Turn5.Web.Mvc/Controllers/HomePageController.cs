using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Turn5.BusinessModel.Models;
using Turn5.BusinessModel.Models.Builders;
using Turn5.Web.WebServices;
using Turn5.WebServices;
using System.Configuration;
using System.Data.SqlClient;

namespace Turn5.Web.Mvc.Controllers
{
    public class HomePageController : BaseController
    {
        protected ITestService _testService;

        public HomePageController(ITestService testService)
        {
            _testService = testService;
        }

        public ActionResult Get()
        {
            var testSetting = ConfigurationManager.AppSettings["TestSetting"];

            //var testObjects = GetTestObject();

            var response = _testService.GetResponse<TestSearchResult>();
            var searchResult = response.Value as TestSearchResult;
            //var searchId = searchResult.SearchId;
            //var products = searchResult.Products;

            var builder = new HomeModelBuilder();
            var model = new HomeModel();
            model = builder.CreateModel();
            return View("HomePage", model);
        }

        public List<TestObject> GetTestObject()
        {
            var connString = ConfigurationManager.ConnectionStrings["SandboxConnection"].ConnectionString;
            
            var sqlConn = new SqlConnection(connString);
            var sqlComm = new SqlCommand 
                                    { 
                                        CommandType = System.Data.CommandType.Text,
                                        CommandText = "SELECT * FROM TestTable", 
                                        Connection = sqlConn 
                                    };
            sqlConn.Open();

            var tos = new List<TestObject>();
            var reader = sqlComm.ExecuteReader();
            while (reader.Read())
            {
                var to = new TestObject();
                to.Id = InferInt(reader["TestId"]);
                to.FirstName = InferString(reader["TestFirstName"]);
                to.LastName = InferString(reader["TestLastName"]);
                tos.Add(to);
            }
            return tos;
        }

        public int InferInt(object obj)
        {
            if (obj is int) return Convert.ToInt32(obj);
            return 0;
        }

        public string InferString(object obj)
        {
            return obj.ToString();
        }
    }

    public class TestObject
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
