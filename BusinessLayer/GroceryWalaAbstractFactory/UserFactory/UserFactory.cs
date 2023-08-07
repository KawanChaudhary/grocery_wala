using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.UserFactory.UserFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.UserFactory
{
    public class UserFactory : IUserFactory
    {
        public UserFactory(IUserFacade userFacade)
        {
            UserFacade = userFacade;
        }

        public IUserFacade UserFacade { get; }
    }
}
