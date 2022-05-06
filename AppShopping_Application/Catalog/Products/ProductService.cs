using AppShopping_Application.Common;
using AppShopping_Data.EF;
using AppShopping_Data.Entities;
using AppShopping_Utilities.Exceptions;
using AppShopping_ViewModels.Catalog.Products;
using AppShopping_ViewModels.Catalog.Reviews;
using AppShopping_ViewModels.Common;
using AppShopping_ViewModels.Utilities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly AppShoppingDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public ProductService(AppShoppingDbContext context, UserManager<AppUser> userManager, IStorageService storageService)
        {
            _context = context;
            _userManager = userManager;
            _storageService = storageService;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<int> AddReview(ProductDetailViewModel model)
        {
            Guid userGuid = new Guid(model.UserCommentId.ToString());
            Review review = new Review()
            {
                ProductId = model.ProductId,
                Comments = model.Review,
                Rating = model.Rating,
                PublishedDate = DateTime.Now,
                UserId = userGuid
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return model.ProductId;
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                // id tự tăng
                Name = request.Name,
                CategoryId = request.CategoryId,
                originPrice = request.originPrice,
                Price = request.Price,
                Stock = request.Stock,
                Description = request.Description,
                DateCreated = DateTime.Now
            };

            //Save product image
            if (request.ProductImage != null)
            {
                product.ProductImage = await this.SaveFile(request.ProductImage);
            }
            else
            {
                product.ProductImage = "/user-content/no-image.png";
            }

            _context.Products.Add(product);

            //trả về số lượng bản ghi maybe
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<bool> DecreaseStock(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null) throw new AppShoppingException($"Cannot find product with id: {productId} ");
            product.Stock -= quantity;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new AppShoppingException($"Không thể tìm sản phẩm có ID: {productId}");

            var images = _context.Products.Where(i => i.Id == productId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ProductImage);
            }

            _context.Products.Remove(product);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            var query = from c in _context.Products
                        select new { c };

            return await query.Select(x => new ProductViewModel()
            {
                Id = x.c.Id,
                Name = x.c.Name,
                CategoryId = x.c.CategoryId,
                Description = x.c.Description,
                originPrice = x.c.originPrice,
                Price = x.c.Price,
                Stock = x.c.Stock,
                ProductImage = x.c.ProductImage
            }).ToListAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        select new { p };
            //2. filter
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.p.CategoryId == request.CategoryId);
            }
            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    CategoryId = x.p.CategoryId,
                    Description = x.p.Description,
                    originPrice = x.p.originPrice,
                    Price = x.p.Price,
                    Stock = x.p.Stock,
                    ProductImage = x.p.ProductImage
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        select new { p };
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.Name.Contains(request.Keyword));

            if (request.CategoryId != null && request.CategoryId != 0)
            {
                query = query.Where(x => x.p.CategoryId == request.CategoryId);
            }

            if (!string.IsNullOrEmpty(request.SortOption))
            {
                switch (request.SortOption)
                {
                    case "Tên A-Z":
                        query = query.OrderBy(x => x.p.Name);
                        break;

                    case "Giá thấp đến cao":
                        query = query.OrderBy(x => x.p.Price);
                        break;

                    case "Giá cao đến thấp":
                        query = query.OrderByDescending(x => x.p.Price);
                        break;
                }
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    CategoryId = x.p.CategoryId,
                    Description = x.p.Description,
                    originPrice = x.p.originPrice,
                    Price = x.p.Price,
                    Stock = x.p.Stock,
                    ProductImage = x.p.ProductImage
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<ProductViewModel> GetById(int productId)
        {
            double ratingAverage = 0;
            double total = 0;
            // Lấy danh mục của sản phẩm
            var categories = await (from c in _context.Categories
                                    join p in _context.Products on c.Id equals p.CategoryId
                                    select p.Name).ToListAsync();

            var product = await _context.Products.FindAsync(productId);


            // Lấy danh sách review
            var reviews = _context.Reviews.Where(x => x.ProductId.Equals(productId) && (Status)x.Status == (Status)1).ToList();

            // Lấy danh sách star rating
            //var ratings = _context.Reviews.Where(d => d.ProductId.Equals(productId)).ToList();

            var listOfReviews = new List<ReviewViewModel>();
            foreach (var review in reviews)
            {
                var user = await _userManager.FindByIdAsync(review.UserId.ToString());
                listOfReviews.Add(new ReviewViewModel()
                {
                    Id = review.Id,
                    UserId = review.UserId,
                    UserName = user.Name,
                    ProductId = review.ProductId,
                    Rating = review.Rating,
                    Comments = review.Comments,
                    PublishedDate = review.PublishedDate
                });
            }

            for (int i = 0; i < listOfReviews.Count; i++)
            {
                total += listOfReviews[i].Rating;
            }
            
            if (listOfReviews.Count > 0)
                ratingAverage = total / listOfReviews.Count;

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                originPrice = product.originPrice,
                Price = product.Price,
                DateCreated = product.DateCreated,
                CategoryId = product.CategoryId != 0 ? product.CategoryId : 0,
                Stock = product.Stock,
                Name = product.Name != null ? product.Name : null,
                Description = product.Description != null ? product.Description : null,                               
                ProductImage = product.ProductImage != null ? product.ProductImage : "no-image.jpg",
                Rating = ratingAverage,
                Reviews = listOfReviews
            };
            return productViewModel;
        }

        public async Task<List<ProductViewModel>> GetFeaturedProducts(int take)
        {
            //1. Select join
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        select new { p };

            var data = await query.OrderByDescending(x => x.p.DateCreated).Take(take)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    CategoryId = x.p.CategoryId,
                    Description = x.p.Description,
                    Price = x.p.Price,
                    Stock = x.p.Stock,
                    ProductImage = x.p.ProductImage,
                }).ToListAsync();

            return data;
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null) throw new AppShoppingException($"Không thể tìm sản phẩm có ID: {request.Id} ");

            product.Name = request.Name;
            product.CategoryId = request.CategoryId;
            product.originPrice = request.originPrice;
            product.Price = request.Price;
            product.Description = request.Description;
            product.Stock = request.Stock;

            //Save product image
            if (request.ProductImage != null)
            {
                product.ProductImage = await this.SaveFile(request.ProductImage);
            }
            else
            {
                product.ProductImage = "/user-content/no-image.png";
            }

            return await _context.SaveChangesAsync();
        }
    }
}
