using GroceryWala.DataServiceLayer.Services.Interface;
using GroceryWala.DomainLayer.Models.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.ProductFactory.EditFacade
{
    public class EditFacade : IEditFacade
    {
        private readonly IProductService productService;

        public EditFacade(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<int> UpdateProduct(ProductModel product)
        {
            return await productService.UpdateProduct(product);
        }

        public async Task DeleteImageById(int imageId)
        {
            await productService.DeleteImageById(imageId);
        }

        public async Task DeleteProductById(int productId)
        {
            await productService.DeleteProductById(productId);
        }

    }
}
