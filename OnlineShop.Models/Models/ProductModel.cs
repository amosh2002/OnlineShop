using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductPhotoURL { get; set; }
    }
}
