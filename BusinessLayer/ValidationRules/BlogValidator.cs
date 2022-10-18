using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class BlogValidator : AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(x => x.BlogTitle).NotEmpty().WithMessage("Başlık alanı boş olamaz.");
            RuleFor(x => x.BlogContent).NotEmpty().WithMessage("İçerik alanı boş olamaz.");
            RuleFor(x => x.BlogImage).NotEmpty().WithMessage("Lütfen resim ekleyiniz.");
            RuleFor(x => x.BlogThumbnailImage).NotEmpty().WithMessage("Lütfen thumbnail ekleyiniz.");
            RuleFor(x => x.BlogTitle).MinimumLength(5).WithMessage("Lütfen 5 karakterden daha fazla veri girişi yapınız.");
            RuleFor(x => x.BlogContent).MinimumLength(5).WithMessage("Lütfen 5 karakterden daha fazla veri girişi yapınız.");
            RuleFor(x => x.BlogTitle).MaximumLength(100).WithMessage("Lütfen 100 karakterden daha az veri girişi yapınız.");
        }
    }
}
