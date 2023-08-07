using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.AdminFactory;
using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.UserFactory;

namespace GroceryWala.BusinessLayer.GroceryWalaAbstractFactory
{
    public interface IGroceryWalaAbstractFactory
    {
        IAdminFactory AdminFactory { get; }

        IUserFactory UserFactory { get; }
    }
}