using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly StoreDataContext _context;

        public ProductController(StoreDataContext context)
        {
            _context = context;
        }

        [Route("v1/products"), HttpGet]
        public IEnumerable<ListProductViewModel> Get()
        {
            return _context.Products.Include(x => x.Category).Select(x => new ListProductViewModel
            {
                Id = x.Id,
                Category = x.Category.Title,
                CategoryId = x.CategoryId,
                Price = x.Price,
                Title = x.Title
            }).AsNoTracking().ToList();
        }

        [Route("v1/products"), HttpPost]
        public ResultViewModel Post([FromBody]EditorProductViewModel model)
        {
            Product product = model.ConvertNewViewModelToModel();
            _context.Products.Add(product);
            _context.SaveChanges();
            
            return new ResultViewModel
            {
                Data = product,
                Message = "Successfull to register Product",
                Success = true
            };
        }

        [Route("v1/products"), HttpPut]
        public ResultViewModel Put([FromBody]EditorProductViewModel model)
        {
            Product product = _context.Products.Find(model.Id);
            product = model.ConvertUsedViewModelToModel(product);
            _context.Entry<Product>(product).State = EntityState.Modified;
            _context.SaveChanges();

            return new ResultViewModel
            {
                Data = product,
                Message = "Successfull to update Product",
                Success = true
            };
        }
    }
}