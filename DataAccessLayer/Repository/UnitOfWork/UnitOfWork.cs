using GroceryWala.DataAccessLayer.Repository.Concrete;
using GroceryWala.DataAccessLayer.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace GroceryWala.DataAccessLayer.Repository.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly GroceryWalaContext _context;

        public IProductRepository ProductRepository { get; set; }
        public IImageRepository ImageRepository { get; set; }
        public ICartItemRepository CartItemRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public IRatingRepository RatingRepository { get; set; }
        public IOfferRepository OfferRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IUserOrderRepository UserOrderRepository { get; set; }

        public UnitOfWork(GroceryWalaContext context)
        {
            _context = context;

            ProductRepository = new ProductRepository(context);
            ImageRepository = new ImageRepository(context);
            CartItemRepository = new CartItemRepository(context);
            CommentRepository = new CommentRepository(context);
            RatingRepository = new RatingRepository(context);
            OfferRepository = new OfferRepository(context);
            OrderRepository = new OrderRepository(context);
            UserOrderRepository = new UserOrderRepository(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
