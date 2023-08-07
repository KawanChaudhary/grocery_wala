using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.AdminFactory.GetFacade;
using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.ProductFactory.AddFacade;
using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.ProductFactory.EditFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.AdminFactory
{
    public class AdminFactory : IAdminFactory
    {
        public IAddFacade AddFacade { get; set; }

        public IGetFacade GetFacade { get; set; }

        public IEditFacade EditFacade { get; set; }

        public AdminFactory(IAddFacade addFacade, IEditFacade editFacade, IGetFacade getFacade)
        {
            AddFacade = addFacade;
            EditFacade = editFacade;
            GetFacade = getFacade;
        }


    }
}
