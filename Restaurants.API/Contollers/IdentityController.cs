using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands;

namespace Restaurants.API.Contollers;

[Route("api/[controller]")]
[ApiController]
public class IdentityController(IMediator _mediator) : ControllerBase
{
    [HttpPatch("user")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
