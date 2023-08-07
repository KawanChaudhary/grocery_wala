using GroceryWala.DataAccessLayer.Repository.Interface;
using System.Threading.Tasks;

namespace GroceryWala.DataAccessLayer.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; set; }

        IImageRepository ImageRepository { get; set; }
        ICartItemRepository CartItemRepository { get; set; }
        ICommentRepository CommentRepository { get; set; }

        IRatingRepository RatingRepository { get; set; }

        IOfferRepository OfferRepository { get; set; }

        IOrderRepository OrderRepository { get; set; }
        IUserOrderRepository UserOrderRepository { get; set; }



        Task CompleteAsync();
        void Dispose();
    }
}