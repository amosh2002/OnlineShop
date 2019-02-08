using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ml.DeleteSelectedProduct(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            ProductModel product = new ProductModel();
            product = ml.FoundProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductModel product)
        {
        
            if (ModelState.IsValid)
            {
                try
                {
                    ml.SaveEditedProduct(product);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public IActionResult Details(int id)
        {

            var student = ml.FoundProduct(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

    }
}