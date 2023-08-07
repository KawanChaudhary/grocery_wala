using GroceryWala.DomainLayer.Models.Single;
using System.Threading.Tasks;

namespace GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.ProductFactory.EditFacade
{
    public interface IEditFacade
    {
        Task DeleteImageById(int imageId);
        Task DeleteProductById(int productId);
        Task<int> UpdateProduct(ProductModel product);
    }
}