using GroceryWala.DomainLayer.Models.Single;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryWala.DataServiceLayer.Services.Interface
{
    public interface IProductService
    {
        Task<int> AddNewProduct(ProductModel product);

        Task<int> UpdateProduct(ProductModel product);

        Task<ImageModel> AddImages(ImageModel image);

        Task<List<ProductModel>> GetAllProductsDetails();
        Task<List<ImageModel>> GetAllProductsImages();
        Task<List<ProductModel>> GetProductsByCategory(int category);

        Task<ProductModel> GetProductById(int productId);

        ImageModel GetProductImage(string productId);

        Task<int> AddComment(CommentModel comment);

        Task<List<CommentModel>> GetCommentsByProductIId(string productId);

        Task<int> AddRating(RatingModel rating);

        RatingModel GetRatingOfProductByuser(string productId, string userId);

        Task<List<OfferModel>> GetAllOffers();
        Task<int> AddOffer(OfferModel offer);

        Task DeleteImageById(int imageId);

        Task DeleteProductById(int productId);
    }
}
