using GroceryWala.DataAccessLayer.Repository.Interface;
using GroceryWala.DomainLayer.Entities;

namespace GroceryWala.DataAccessLayer.Repository.Concrete
{
    public class RatingRepository : GenericRepository<RatingEntity>, IRatingRepository
    {
        public RatingRepository(GroceryWalaContext context) : base(context) { }
    }
}
