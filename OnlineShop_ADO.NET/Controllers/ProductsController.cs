using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;

namespace OnlineShop_ADO.NET.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MainLogic ml = null;
        public ProductsController()
        {
            ml = new MainLogic();
        }
        public IActionResult Index()
        {
            List<ProductModel> prod = new List<ProductModel>();
            prod = ml.GetAllProducts();
            return View(prod);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductModel prod)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ml.InsertNewProduct(prod);
                }
            }

            catch (NullReferenceException ex)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, new { ErrorMessage = "Vat Baner mi Ara" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErroMessage = ex.Message, Code = StatusCodes.Status400BadRequest });
            }
            return RedirectToAction(nameof(Index));
        }
    }
}