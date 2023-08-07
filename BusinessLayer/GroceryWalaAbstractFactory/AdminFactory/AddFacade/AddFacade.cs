using GroceryWala.DataServiceLayer.Services.Interface;
using GroceryWala.DomainLayer.Models.Single;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.ProductFactory.AddFacade
{
    public class AddFacade : IAddFacade
    {
        private readonly IProductService productService;
        private readonly IWebHostEnvironment webHostEnvironement;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AddFacade(IProductService productService, IWebHostEnvironment _webHostEnvironement,
            IHttpContextAccessor _httpContextAccessor)
        {
            this.productService = productService;
            this.httpContextAccessor = _httpContextAccessor;
            this.webHostEnvironement = _webHostEnvironement;

        }

        public async Task<int> AddNewProduct(ProductModel product)
        {
            return await productService.AddNewProduct(product);
        }

        public async Task AddImages(IFormFile[] images, string productId)
        {

            var baseUrl = httpContextAccessor.HttpContext.Request.Scheme + "://" +
                httpContextAccessor.HttpContext.Request.Host +
                httpContextAccessor.HttpContext.Request.PathBase;
            string directory = $"{webHostEnvironement.WebRootPath}\\images\\{productId}";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            foreach (var file in images)
            {

                var path = Path.Combine(directory, file.FileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                var image = new ImageModel()
                {
                    ProductId = productId,
                    ImageAddress = path
                };

                await productService.AddImages(image);

            }
        }

        public async Task<int> AddOffers(OfferModel offer)
        {
            return await productService.AddOffer(offer);
        }
    }
}
