using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.AdminFactory;
using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.UserFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.BusinessLayer.GroceryWalaAbstractFactory
{
    public class GroceryWalaAbstractFactory : IGroceryWalaAbstractFactory
    {

        public GroceryWalaAbstractFactory(IAdminFactory adminFactory, IUserFactory userFactory)
        {
            AdminFactory = adminFactory;
            UserFactory = userFactory;
        }

        public IAdminFactory AdminFactory { get; }
        public IUserFactory UserFactory { get; }
    }
}
