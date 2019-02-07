using DAL;
using OnlineShop.Models;
using System;
using System.Collections.Generic;

namespace BL
{
    public class MainLogic
    {
        private readonly ShopDataContext db = null;
        public MainLogic()
        {
            db = new ShopDataContext();
        }
        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> products = new List<ProductModel>();
            products = db.GetProducts();
            foreach (ProductModel product in products)
            {
                //if (product.Id == 0)
                //{
                //    product.ProductName = "Invalid";
                //    product.ProductPrice = 0;
                //    product.ProductPhotoURL = "Invalid";
                //}
            }
            return products;
        }
        public void InsertNewProduct(ProductModel product)
        {
            if (!string.IsNullOrWhiteSpace(product.ProductName) || product.ProductPrice > 0)
            {
                db.InsertProduct(product);
            }
            else
            {
                throw new Exception("Cannot insert null value");
            }
        }
    }
}
