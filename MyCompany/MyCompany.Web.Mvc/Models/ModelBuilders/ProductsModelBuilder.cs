using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MyCompany.Mvc.Models;

namespace MyCompany.Web.Mvc.Models.ModelBuilders
{
    public class ProductsModelBuilder 
    {
        public ProductsModel CreateModel(int? page, int? pageSize)
        {
            var sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AMConnection"].ConnectionString);
            var sqlComm = new SqlCommand
            {
                Connection = sqlConn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "spGetProducts"
            };

            SqlParameter param;
            if (page != null)
            {
                param = new SqlParameter { ParameterName = "PAGE", Value = page };
                sqlComm.Parameters.Add(param);
            }

            if (pageSize != null)
            {
                param = new SqlParameter { ParameterName = "PAGE_SIZE", Value = pageSize };
                sqlComm.Parameters.Add(param);
            }

            sqlConn.Open();
            var reader = sqlComm.ExecuteReader();

            var model = new ProductsModel
            {
                Products = new List<ProductModel>()
            };

            while (reader.Read())
            {
                var product = new ProductModel
                {
                    ProductId = (int)reader["ProductId"],
                    ProductName = reader["ProductName"].ToString(),
                    ProductSKU = reader["ProductSKU"].ToString()
                };
                model.Products.Add(product);
            }
            sqlConn.Close();

            model.Page = page ?? 1;

            return model;
        }
    }
}
