using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Contollers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RestaurantsController(IMediator _mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
    {
        var restaurants = await _mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<RestaurantDto>> GetById([FromRoute] int id)
    {
        var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));
        return Ok(restaurant);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);

    }
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _mediator.Send(new DeleteRestaurantCommand(id));
        return NoContent();
    }
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRestaurantCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);

        return NoContent();

    }

}
