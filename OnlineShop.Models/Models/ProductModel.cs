using System;

namespace OnlineShop.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductPhotoURL { get; set; }
    }
}
