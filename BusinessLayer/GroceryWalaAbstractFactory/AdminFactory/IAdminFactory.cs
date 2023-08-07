using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.AdminFactory.GetFacade;
using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.ProductFactory.AddFacade;
using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.ProductFactory.EditFacade;

namespace GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.AdminFactory
{
    public interface IAdminFactory
    {
        IAddFacade AddFacade { get; set; }
        IEditFacade EditFacade { get; set; }
        IGetFacade GetFacade { get; set; }
    }
}