using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.Common.Response;
using Sat.Recruitment.Application.Users.Commands;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Result>> Create([FromBody] CreateUserCommand user)
        {
            var result = await _mediator.Send(user);
            return Ok(result);
        }
    }
}
