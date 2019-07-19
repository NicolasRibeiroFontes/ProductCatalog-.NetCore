using System;
using Flunt.Notifications;
using Flunt.Validations;
using ProductCatalog.Models;

namespace ProductCatalog.ViewModels.ProductViewModels
{
    public class EditorProductViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .HasMaxLen(Title, 120, "Title", "The title is bigger that is possible!")
                .HasMinLen(Title, 3, "Title", "The title is smaller that is possible!")
                .IsGreaterThan(Price, 0, "Price", "The price need to be greater than 0")
            );
        }

        public Product ConvertNewViewModelToModel()
        {
            return new Product
            {
                Id = Id,
                CategoryId = CategoryId,
                Title = Title,
                Description = Description,
                Price = Price,
                Quantity = Quantity,
                Image = Image,
                CreateDate = new DateTime()
            };
        }

        public Product ConvertUsedViewModelToModel(Product product)
        {
            product.Id = Id;
            product.CategoryId = CategoryId;
            product.Title = Title;
            product.Description = Description;
            product.Price = Price;
            product.Quantity = Quantity;
            product.Image = Image;
            product.LastUpdateDate = new DateTime();
            return product;
        }
    }
}