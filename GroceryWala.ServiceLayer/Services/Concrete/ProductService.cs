using GroceryWala.DataAccessLayer.Repository.UnitOfWork;
using GroceryWala.DataServiceLayer.Services.Interface;
using GroceryWala.DomainLayer.Entities;
using GroceryWala.DomainLayer.Models.Single;
using GroceryWala.DomainLayer.Other;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroceryWala.DataServiceLayer.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> AddNewProduct(ProductModel product)
        {
            var newProduct = new ProductEntity()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                Stock = product.Stock,
                Category = product.Category,
                SizeType = product.SizeType,
                Quantity = product.Quantity,
                OtherDetails = product.OtherDetails,

                ModifiedOn = DateTime.UtcNow,
                CreatedOn = DateTime.UtcNow

            };
            await unitOfWork.ProductRepository.Add(newProduct);
            await unitOfWork.CompleteAsync();

            return newProduct.Id;
        }

        public async Task<int> UpdateProduct(ProductModel product)
        {
            var updateProduct = new ProductEntity()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                Stock = product.Stock,
                Category = product.Category,
                SizeType = product.SizeType,
                Quantity = product.Quantity,
                OtherDetails = product.OtherDetails,
                Rating = product.Rating,
                ReviewCount = product.ReviewCount,
                TotalRatings = product.TotalRatings,
                
                ModifiedOn = DateTime.UtcNow,
            };
            unitOfWork.ProductRepository.Update(updateProduct);
            await unitOfWork.CompleteAsync();

            return updateProduct.Id;
        }

        public async Task<ImageModel> AddImages(ImageModel image)
        {
            var newImage = new ImageEntity()
            {
                ProductId = image.ProductId,
                ImageAddress = image.ImageAddress,
            };

            await unitOfWork.ImageRepository.Add(newImage);
            await unitOfWork.CompleteAsync();
            image.Id = newImage.Id;
            return image;
        }


        public async Task<List<ProductModel>> GetAllProductsDetails()
        {
            var products = await unitOfWork.ProductRepository.All();

            var res = new List<ProductModel>();

            if (products?.Any() == true)
            {
                foreach (var product in products)
                {
                    res.Add(new ProductModel()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Discount = product.Discount,
                        Stock = product.Stock,
                        Category = product.Category,
                        SizeType = product.SizeType,
                        Quantity = product.Quantity,
                        OtherDetails = product.OtherDetails,
                    }); ;
                }
            }
            return res;
        }

        public async Task<List<ImageModel>> GetAllProductsImages()
        {
            var images = await unitOfWork.ImageRepository.All();

            var res = new List<ImageModel>();

            if (images?.Any() == true)
            {
                foreach (var image in images)
                {
                    res.Add(new ImageModel()
                    {
                        Id = image.Id,
                        ProductId = image.ProductId,
                        ImageAddress = image.ImageAddress

                    });
                }
            }
            return res;
        }

        public async Task<List<ProductModel>> GetProductsByCategory(int category)
        {
            var products = await unitOfWork.ProductRepository.All();

            var res = new List<ProductModel>();

            if (products?.Any() == true)
            {
                foreach (var product in products)
                {
                    if (product.Category == (CategoryType)category)
                    {

                        res.Add(new ProductModel()
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Description = product.Description,
                            Price = product.Price,
                            Discount = product.Discount,
                            Stock = product.Stock,
                            Category = product.Category,
                            SizeType = product.SizeType,
                            Quantity = product.Quantity,
                            OtherDetails = product.OtherDetails,
                        }); ;
                    }
                }
            }
            return res;
        }

        public async Task<ProductModel> GetProductById(int productId)
        {

            var product = await unitOfWork.ProductRepository.GetById(productId);

            var resProduct = new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                Stock = product.Stock,
                Category = product.Category,
                SizeType = product.SizeType,
                Quantity = product.Quantity,
                OtherDetails = product.OtherDetails,
                Rating = product.Rating,
                ReviewCount = product.ReviewCount,
                TotalRatings = product.TotalRatings,
   
            };
            return resProduct;
        }

        public ImageModel GetProductImage(string productId)
        {
            Expression<Func<ImageEntity, bool>> condition = entity => entity.ProductId == productId;

            var image = unitOfWork.ImageRepository.FindFirst(condition);

            return new ImageModel()
            {
                Id = image.Id,
                ProductId = image.ProductId,
                ImageAddress = "http://127.0.0.1:8080/images/" + image.ImageAddress.Substring(97)
            };
        }

        public async Task DeleteProductById(int productId)
        {
            var product = await unitOfWork.ProductRepository.Delete(productId);

            Expression<Func<ImageEntity, bool>> condition = entity => entity.ProductId == productId.ToString();

            var images = await unitOfWork.ImageRepository.Find(condition);

            foreach(var image in images)
            {
                await unitOfWork.ImageRepository.Delete(image.Id);
            }

            var path = images.ToList()[0].ImageAddress.Substring(0, 98);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            await unitOfWork.CompleteAsync();
        }

        public async Task DeleteImageById(int imageId)
        {
            var image = await unitOfWork.ImageRepository.GetById(imageId);

            string imagePath = image.ImageAddress;

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            var res = await unitOfWork.ImageRepository.Delete(imageId);
            await unitOfWork.CompleteAsync();
        }

        public async Task<int> AddComment(CommentModel comment)
        {
            var newComment = new CommentEntity()
            {
                UserId = comment.UserId,
                ProductId = comment.ProductId,
                FirstName = comment.FirstName,
                LastName = comment.LastName,
                Text = comment.Text,
                CreatedAt = DateTime.UtcNow
            };

            await unitOfWork.CommentRepository.Add(newComment);

            // Update product total review

            var product = await unitOfWork.ProductRepository.GetById(Int32.Parse(comment.ProductId));
            product.ReviewCount++;

            unitOfWork.ProductRepository.Update(product);

            await unitOfWork.CompleteAsync();

            return newComment.Id;
        }

        public async Task<int> AddRating(RatingModel rating)
        {

            Expression<Func<RatingEntity, bool>> condition = entity =>
            entity.ProductId == rating.ProductId && entity.UserId == rating.UserId;

            var existRating = unitOfWork.RatingRepository.FindFirst(condition);

            if (existRating != null)
            {
                existRating.Rating = rating.Rating;
                var res = unitOfWork.RatingRepository.Update(existRating);

                // Update product total rating

                var product = await unitOfWork.ProductRepository.GetById(Int32.Parse(rating.ProductId));

                product.Rating += rating.Rating;
                product.TotalRatings++;
                unitOfWork.ProductRepository.Update(product);

                await unitOfWork.CompleteAsync();
                return existRating.Id;
            }
            else
            {

                var newRating = new RatingEntity()
                {
                    UserId = rating.UserId,
                    ProductId = rating.ProductId,
                    Rating = rating.Rating
                };

                await unitOfWork.RatingRepository.Add(newRating);
                await unitOfWork.CompleteAsync();

                return newRating.Id;
            }
        }

        public async Task<List<CommentModel>> GetCommentsByProductIId(string productId)
        {

            Expression<Func<CommentEntity, bool>> condition = entity => entity.ProductId == productId;

            var comments = await unitOfWork.CommentRepository.Find(condition);

            var res = new List<CommentModel>();

            if (comments.Any())
            {
                foreach (var comment in comments)
                {
                    res.Add(new CommentModel()
                    {
                        Id = comment.Id,
                        UserId = comment.UserId,
                        ProductId = comment.ProductId,
                        FirstName = comment.FirstName,
                        LastName = comment.LastName,
                        Text = comment.Text,
                        CreatedAt = comment.CreatedAt
                    });
                }
            }
            return res;
        }

        public RatingModel GetRatingOfProductByuser(string productId, string userId)
        {

            Expression<Func<RatingEntity, bool>> condition = entity => 
            entity.ProductId == productId && entity.UserId == userId;

            var existRating = unitOfWork.RatingRepository.FindFirst(condition);

            if(existRating is null)
            {
                return null;
            }

            var res = new RatingModel()
            {
                Id = existRating.Id,
                UserId = existRating.UserId,
                ProductId = existRating.ProductId,
                Rating = existRating.Rating
            };

            return res;
        }

        public async Task<int> AddOffer(OfferModel offer)
        {
            var OfferEntity = new OfferEntity()
            {
                OfferCode = offer.OfferCode,
                Description = offer.Description,
                Price = offer.Price,
                OffPrice = offer.OffPrice,
                StartDate = offer.StartDate,
                EndDate = offer.EndDate,
                IsActive = true
            };

            var res = await unitOfWork.OfferRepository.Add(OfferEntity);
            await unitOfWork.CompleteAsync();
            return OfferEntity.Id;
        }

        public async Task<List<OfferModel>> GetAllOffers()
        {
            var offers = await unitOfWork.OfferRepository.All();

            var res = new List<OfferModel>();

            if (offers.Any()){
                foreach(var offer in offers)
                {
                    res.Add(new OfferModel()
                    {
                        Id = offer.Id,
                        OfferCode = offer.OfferCode,
                        Description = offer.Description,
                        Price = offer.Price,
                        OffPrice = offer.OffPrice,
                        StartDate = offer.StartDate,
                        EndDate = offer.EndDate,
                        IsActive = offer.IsActive
                    });
                }
            }
            return res;
        }

    }
}
