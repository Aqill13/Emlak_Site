using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidatorRules
{
    public class AdvertValidation : AbstractValidator<Advert>
    {
        public AdvertValidation()
        {
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address girin");
            RuleFor(x => x.Area).NotEmpty().WithMessage("Area girin");
            RuleFor(x => x.SituationId).NotEmpty().WithMessage("Situation seçin");
            RuleFor(x => x.CityId).NotEmpty().WithMessage("City seçin");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description yazın");
            RuleFor(x => x.DistrictId).NotEmpty().WithMessage("District seçin");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title yazın");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Image daxil edin");
            RuleFor(x => x.TypeId).NotEmpty().WithMessage("Type seçin");
            RuleFor(x => x.NumberOfRooms).NotEmpty().WithMessage("Mərtəbə seçin");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price girin");
            RuleFor(x => x.PhoneNumber).MaximumLength(10).MinimumLength(10).NotEmpty().WithMessage("10 rəqəmli telefon nömrəsini düzgün girin");
        }
    }
}
