using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication12.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCountry { get; set; }
    }

    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class NorthwindDb
    {
        private string _connectionString;

        public NorthwindDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Order> GetOrders()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Orders";
            List<Order> orders = new List<Order>();
            
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                orders.Add(new Order
                {
                    Id = (int)reader["OrderId"],
                    OrderDate = (DateTime)reader["OrderDate"],
                    ShipAddress = (string)reader["ShipAddress"],
                    ShipCountry = (string)reader["ShipCountry"],
                    ShipName = (string)reader["ShipName"]
                });
            }

            return orders;
        }

        public List<Order> GetOrdersByYear(int year, string country)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Orders wHERE DATEPART(Year, OrderDate) = @year " +
                "AND ShipCountry = @country";
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@country", country);
            List<Order> orders = new List<Order>();

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                orders.Add(new Order
                {
                    Id = (int)reader["OrderId"],
                    OrderDate = (DateTime)reader["OrderDate"],
                    ShipAddress = (string)reader["ShipAddress"],
                    ShipCountry = (string)reader["ShipCountry"],
                    ShipName = (string)reader["ShipName"]
                });
            }

            return orders;
        }

        public List<OrderDetail> GetOrderDetails(int year)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT od.* FROM [Order Details] od " +
                "JOIN Orders o " +
                "ON o.OrderId = od.OrderId " +
                "WHERE DATEPART(YEAR, o.OrderDate) = @year";
            cmd.Parameters.AddWithValue("@year", year);
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                orderDetails.Add(new OrderDetail
                {
                    OrderId = (int)reader["OrderId"],
                    ProductId = (int)reader["ProductId"],
                    Quantity = (short)reader["Quantity"],
                    UnitPrice = (decimal)reader["UnitPrice"],
                });
            }

            return orderDetails;
        }

        public List<Category> GetCategories()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Categories";
            List<Category> categories = new List<Category>();

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new Category
                {
                    Id = (int)reader["CategoryId"],
                    Name = (string)reader["CategoryName"],
                    Description = (string)reader["Description"]
                });
            }

            return categories;
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Products WHERE CategoryId = @categoryId";
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            List<Product> products = new List<Product>();

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = (int)reader["ProductId"],
                    Name = (string)reader["ProductName"],
                    QuantityPerUnit = (string)reader["QuantityPerUnit"],
                    UnitPrice = (decimal)reader["UnitPrice"]
                });
            }

            return products;
        }

        public string GetCategoryName(int categoryId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT CategoryName FROM Categories WHERE CategoryId = @categoryId";
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            connection.Open();
            return (string)cmd.ExecuteScalar();
        }

        public List<Product> SearchProducts(string searchText)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Products WHERE ProductName LIKE @searchText";
            cmd.Parameters.AddWithValue("@searchText", $"%{searchText}%");
            List<Product> products = new List<Product>();

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = (int)reader["ProductId"],
                    Name = (string)reader["ProductName"],
                    QuantityPerUnit = (string)reader["QuantityPerUnit"],
                    UnitPrice = (decimal)reader["UnitPrice"]
                });
            }

            return products;
        }
    }
}