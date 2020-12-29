﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using web_api.Classes;
using web_api.Models;

namespace web_api.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShopContext _context;

        public ProductController(ShopContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();
        }

        //[Route("[controller]")]
        //[HttpGet]
        //public IEnumerable<Product> GetAllProducts() {

        //    return _context.Products.ToArray();
        //}
        //------------------------------------------------------------------------
        //[Route("[controller]")]
        //[HttpGet]
        //public async Task<IActionResult> GetAllProducts()
        //{
        //    var products = await _context.Products.ToArrayAsync();
        //    return Ok(products);
        //}
        //------------------------------------------------------------------------
        // pagination example
        // https://exapmle.com/products?size=10&page=2

        [Route("[controller]")]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParameters queryParameters)
        {
            IQueryable<Product> products = _context.Products;

            if (queryParameters.MinPrice != null &&
                 queryParameters.MaxPrice != null)
            {
                products = products.Where(
                    p => p.Price >= queryParameters.MinPrice.Value &&
                         p.Price <= queryParameters.MaxPrice.Value);
            }
            if (!string.IsNullOrEmpty(queryParameters.Sku))
            {
                products = products.Where(p => p.Sku == queryParameters.Sku);
            }

            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                products = products.Where(p => p.Name.ToLower().Contains(queryParameters.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(queryParameters.SortBy))
            {
                if (typeof(Product).GetProperty(queryParameters.SortBy) != null)
                {
                    products = products.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);
                }
            }

            products = products
                .Skip(queryParameters.Size * (queryParameters.Page - 1))
                .Take(queryParameters.Size);

            products = products.Skip(queryParameters.Size * (queryParameters.Page - 1))
                .Take(queryParameters.Size);

            return Ok(await products.ToArrayAsync());
        }
        //------------------------------------------------------------------------

        //[HttpGet, Route("/product/{id}")]
        //public Product GetProductById(int id)
        //{
        //    var product = _context.Products.Find(id);
        //    return product;
        //}
        //------------------------------------------------------------------------

        [HttpGet, Route("/product/{id:int}")]
        public async Task <IActionResult> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("message: This Product does not exist");
            }
            else
                return Ok(product);
        }
        //------------------------------------------------------------------------

    }
}
