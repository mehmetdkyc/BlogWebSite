using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Başlık alanı boş olamaz.");
            RuleFor(x => x.CategoryDescription).NotEmpty().WithMessage("İçerik alanı boş olamaz.");
            RuleFor(x => x.CategoryName).MinimumLength(5).WithMessage("Lütfen 2 karakterden daha fazla veri girişi yapınız.");
            RuleFor(x => x.CategoryDescription).MaximumLength(250).WithMessage("Lütfen 100 karakterden daha az veri girişi yapınız.");
        }
    }
}
