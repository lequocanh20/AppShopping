using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_ViewModels.Catalog.Categories
{
    public class CategoryUpdateRequest
    {
        public int Id { set; get; }

        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }

        [Display(Name = "Hình ảnh danh mục")]
        public IFormFile ImageCate { get; set; }
    }
}
