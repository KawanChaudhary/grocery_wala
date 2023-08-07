using GroceryWala.DataAccessLayer.Repository.Interface;
using GroceryWala.DomainLayer.Entities;

namespace GroceryWala.DataAccessLayer.Repository.Concrete
{
    public class ProductRepository: GenericRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(GroceryWalaContext context) : base(context) { }
    }
}
