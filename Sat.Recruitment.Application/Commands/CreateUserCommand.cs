using MediatR;
using Sat.Recruitment.Application.Common.Response;

namespace Sat.Recruitment.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }
    }
}
