using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication12.Models;

namespace WebApplication12.Controllers
{

    public class NorthwindController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=Northwnd;Integrated Security=true;";

        public ActionResult Orders()
        {
            NorthwindDb db = new NorthwindDb(_connectionString);

            List<Order> orders = db.GetOrders();
            OrdersPageViewModel vm = new OrdersPageViewModel
            {
                Orders = orders,
                CurrrentDate = DateTime.Now
            };

            return View(vm);
        }

        public ActionResult OrdersByYear(int year, string country)
        {
            NorthwindDb db = new NorthwindDb(_connectionString);

            List<Order> orders = db.GetOrdersByYear(year, country);
            OrdersByYearViewModel vm = new OrdersByYearViewModel
            {
                Orders = orders,
                Year = year,
                Country = country
            };

            return View(vm);
        }

        public ActionResult OrderDetails(int year)
        {
            NorthwindDb db = new NorthwindDb(_connectionString);
            List<OrderDetail> orderDetails = db.GetOrderDetails(year);

            return View(orderDetails);
        }

        public ActionResult Categories()
        {
            NorthwindDb db = new NorthwindDb(_connectionString);
            CategoriesPageViewModel vm = new CategoriesPageViewModel
            {
                Categories = db.GetCategories()
            };

            return View(vm);
        }

        public ActionResult Products(int categoryId = 1)
        {
            NorthwindDb db = new NorthwindDb(_connectionString);
            ProductsPageViewModel vm = new ProductsPageViewModel
            {
                Products = db.GetProductsByCategory(categoryId),
                CategoryName = db.GetCategoryName(categoryId)
            };

            return View(vm);
        }

        public ActionResult ProductSearch()
        {
            return View();
        }

        public ActionResult ProductSearchResults(string searchText)
        {
            NorthwindDb db = new NorthwindDb(_connectionString);
            ProductsSearchViewModel vm = new ProductsSearchViewModel
            {
                Products = db.SearchProducts(searchText),
                SearchText = searchText
            };

            return View(vm);
        }
    }
}

//Create an application that has two pages:
// /northwind/categories
// /northwind/products

//On the categories page, display a list of all categories in the northwind database
//(id, name, description). The name of the category should be a link, that when clicked
//takes the user to the products page. On the products page, only the products
//for the category that was clicked on should be displayed. Additionally, on top of
//the products page, have an H1 that says "Products for Category {CategoryName}"