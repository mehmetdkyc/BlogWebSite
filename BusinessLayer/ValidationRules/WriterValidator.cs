using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Writer name or surname can't be null");
            RuleFor(x => x.MailAdress).NotEmpty().WithMessage("Mail Adress can't be null");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password can't be null");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Name length can't be less than 2 character");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Name length can't be higher than 50 character");
        }
    }
}
