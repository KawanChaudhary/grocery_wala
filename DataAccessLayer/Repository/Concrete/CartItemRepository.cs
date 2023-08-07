using GroceryWala.DataAccessLayer.Repository.Interface;
using GroceryWala.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DataAccessLayer.Repository.Concrete
{
    public class CartItemRepository : GenericRepository<CartItemEntity>, ICartItemRepository
    {
        public CartItemRepository(GroceryWalaContext context) : base(context) { }
    }
}
