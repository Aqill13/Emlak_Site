using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidatorRules
{
    public class SituationValidation:AbstractValidator<Situation>
    {
        public SituationValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name girin");
        }
    }
}
