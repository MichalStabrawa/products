using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

public class ProductsController : Controller
{
    public string getSomeText()
    {
        return "ale ma kota";
    }

    public string[] getSomeArray()
    {
        return new string[] { "pierwszy string" };
    }

    public string[] GetVeggies()
    {
        return new string[]{
    "pomidor",
    "marchew",
    "jablko"
};
    }

    public List<Product> GetAllProducts(string searchedPhrase)
    {
        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = "Server=LAPTOP-2AOO7D4S\\SQLEXPRESS;Database=TSQL2012;Trusted_Connection=True;";
        connection.Open();
        SqlCommand command = new SqlCommand();

        command.CommandType = CommandType.Text;
        command.CommandText = $"SELECT * FROM GetALLProducts WHERE productname like '%{searchedPhrase}%'";
        command.Connection = connection;

        SqlDataReader reader = command.ExecuteReader();

        List<Product> listProduct = new List<Product>();
        while (reader.Read())
        {
            Product temp = new Product();
            temp.ProductID = int.Parse(reader["productid"].ToString());
            temp.ProductName = reader["productName"].ToString();
            temp.SupplierID = int.Parse(reader["supplierid"].ToString());
            temp.CategoryID = int.Parse(reader["categoryID"].ToString());
            temp.UnitPrice = double.Parse(reader["unitprice"].ToString());
            temp.Discontinued = bool.Parse(reader["discontinued"].ToString());
            listProduct.Add(temp);
        }

        return listProduct;
    }
}

