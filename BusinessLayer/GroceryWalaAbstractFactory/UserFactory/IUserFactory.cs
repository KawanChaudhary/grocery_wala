using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.UserFactory.UserFacade;

namespace GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.UserFactory
{
    public interface IUserFactory
    {
        IUserFacade UserFacade { get; }
    }
}