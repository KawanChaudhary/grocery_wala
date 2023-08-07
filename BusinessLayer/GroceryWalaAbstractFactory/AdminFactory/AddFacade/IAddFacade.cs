using GroceryWala.DomainLayer.Models.Single;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.ProductFactory.AddFacade
{
    public interface IAddFacade
    {
        Task AddImages(IFormFile[] images, string productId);
        Task<int> AddNewProduct(ProductModel product);
        Task<int> AddOffers(OfferModel offer);
    }
}