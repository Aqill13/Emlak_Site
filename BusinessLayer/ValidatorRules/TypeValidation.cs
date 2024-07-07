using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidatorRules
{
    public class TypeValidation:AbstractValidator<EntityLayer.Entities.Type>
    {
        public TypeValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Type name girin");
        }
    }
}
