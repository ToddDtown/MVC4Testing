using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using MyCompany.Web.Mvc.Models.ModelBuilders;

namespace MyCompany.Web.Mvc.Controllers
{
    public class ProductsController : BaseController
    {
        public ActionResult Get(int? page, int? pageSize)
        {
            var modelBuilder = new ProductsModelBuilder();
            var model = modelBuilder.CreateModel(page, pageSize);
            
            return View("Products", model);
        }
        
        public ActionResult GenerateProducts()
        {
            var sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AMConnection"].ConnectionString);
            var sqlComm = new SqlCommand
            {
                Connection = sqlConn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "spGenerateProduct"
            };

            SqlParameter param;
            param = new SqlParameter {ParameterName = "PRODUCT_NAME"};
            sqlComm.Parameters.Add(param);

            param = new SqlParameter {ParameterName = "PRODUCT_SKU"};
            sqlComm.Parameters.Add(param);

            sqlConn.Open();
            var tran = sqlConn.BeginTransaction();
            sqlComm.Transaction = tran;

            try
            {
                for (var i = 1; i <= 100; i++)
                {
                    sqlComm.Parameters["PRODUCT_NAME"].Value = "Product " + i.ToString();
                    sqlComm.Parameters["PRODUCT_SKU"].Value = i.ToString("D3");
                    sqlComm.ExecuteNonQuery();
                }

                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            finally
            {
                sqlConn.Close();
            }

            return View("Products", null);
        }
    }
}
