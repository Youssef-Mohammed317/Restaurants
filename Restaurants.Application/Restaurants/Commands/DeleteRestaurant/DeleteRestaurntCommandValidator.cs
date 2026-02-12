using FluentValidation;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurntCommandValidator : AbstractValidator<DeleteRestaurantCommand>
{
    public DeleteRestaurntCommandValidator()
    {

        RuleFor(x => x.Id).Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Invalid restaurant id.");
    }
}
