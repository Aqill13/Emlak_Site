using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidatorRules
{
    public class DistrictValidation:AbstractValidator<District>
    {
        public DistrictValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name girin");
            RuleFor(x => x.CityId).NotEmpty().WithMessage("City seçin");
        }
    }
}
