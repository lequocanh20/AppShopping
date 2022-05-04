using AppShopping_Data.EF;
using AppShopping_Data.Entities;
using AppShopping_ViewModels.Catalog.Products;
using AppShopping_ViewModels.Catalog.Reviews;
using AppShopping_ViewModels.Utilities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly AppShoppingDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ProductService(AppShoppingDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                OriginPrice = x.c.OriginPrice,
                Price = x.c.Price,
                Stock = x.c.Stock,
                ProductImage = x.c.ProductImage
            }).ToListAsync();
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

            ratingAverage = total / listOfReviews.Count;

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name != null ? product.Name : null,
                CategoryId = product.CategoryId != 0 ? product.CategoryId : 0,
                Description = product.Description != null ? product.Description : null,
                OriginPrice = product.OriginPrice,
                Price = product.Price,
                Stock = product.Stock,
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
    }
}
