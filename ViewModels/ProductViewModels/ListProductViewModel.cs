using ProductCatalog.Models;

namespace ProductCatalog.ViewModels.ProductViewModels
{
    public class ListProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }

        public ListProductViewModel(Product product)
        {
            Id = product.Id;
            Title = product.Title;
            Price = product.Price;
            CategoryId = product.CategoryId;
            Category = product.Category.Title;
        }
    }
}