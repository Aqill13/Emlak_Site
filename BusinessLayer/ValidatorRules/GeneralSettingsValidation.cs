using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidatorRules
{
    public class GeneralSettingsValidation:AbstractValidator<GeneralSettings>
    {
        public GeneralSettingsValidation()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Düzgün email yazın");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address girin");
            RuleFor(x => x.PhoneNumber).MaximumLength(10).MinimumLength(10).NotEmpty().WithMessage("10 rəqəmli telefon nömrəsini düzgün girin");
        }
    }
}
