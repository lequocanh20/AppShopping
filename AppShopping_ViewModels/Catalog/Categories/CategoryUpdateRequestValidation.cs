﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_ViewModels.Catalog.Categories
{
    public class CategoryUpdateRequestValidation : AbstractValidator<CategoryCreateRequest>
    {
        public CategoryUpdateRequestValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên danh mục không được để trống")
                .MaximumLength(200).WithMessage("Tên danh mục không được vượt quá 200 kí tự");
        }
    }
}
