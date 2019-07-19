using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/products"), HttpGet, ResponseCache(Location = ResponseCacheLocation.Any, Duration = 3600)] //Cache 1hour
        public IEnumerable<ListProductViewModel> Get()
        {
            return _repository.Get().Select(x => new ListProductViewModel(x)).ToList();
        }

        [Route("v1/products/{id}"), HttpGet, ResponseCache(Duration = 3600)]
        public ListProductViewModel Get(int id)
        {
            return new ListProductViewModel(_repository.Get(id));
        }

        [Route("v1/products"), HttpPost]
        public ResultViewModel Post([FromBody]EditorProductViewModel model)
        {
            model.Validate();
            if (model.Invalid)
                return new ResultViewModel
                {
                    Data = model.Notifications,
                    Message = "Wasn't possible save product!",
                    Success = false
                };

            Product product = model.ConvertNewViewModelToModel();
            _repository.Save(product);

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
            model.Validate();
            if (model.Invalid)
                return new ResultViewModel
                {
                    Data = model.Notifications,
                    Message = "Wasn't possible update product!",
                    Success = false
                };

            Product product = _repository.Get(model.Id);
            product = model.ConvertUsedViewModelToModel(product);
            _repository.Update(product);

            return new ResultViewModel
            {
                Data = product,
                Message = "Successfull to update Product",
                Success = true
            };
        }
    }
}