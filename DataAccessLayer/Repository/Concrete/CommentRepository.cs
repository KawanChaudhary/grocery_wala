using GroceryWala.DataAccessLayer.Repository.Interface;
using GroceryWala.DomainLayer.Entities;

namespace GroceryWala.DataAccessLayer.Repository.Concrete
{
    public class CommentRepository : GenericRepository<CommentEntity>, ICommentRepository
    {
        public CommentRepository(GroceryWalaContext context) : base(context) { }
    }
}
