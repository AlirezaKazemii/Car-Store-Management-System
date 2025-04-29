using Application.Commands.Car;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Fluent_Validation.Car
{
    public class DeleteCarByIdCommandValidator : AbstractValidator<DeleteCarByIdCommand>
    {
        public DeleteCarByIdCommandValidator()
        {
            RuleFor(x => x.CarId).NotEmpty().WithMessage("CarId is required.");
        }
    }
}
