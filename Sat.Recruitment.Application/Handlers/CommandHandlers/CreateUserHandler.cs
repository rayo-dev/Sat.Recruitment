using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Configuration;
using Sat.Recruitment.Application.Common.Exceptions;
using Sat.Recruitment.Application.Common.Mappings;
using Sat.Recruitment.Application.Common.Response;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Application.Users.Commands;
using Sat.Recruitment.Core.Strategies;
using Sat.Recruitment.Core.Validators;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Handlers.CommandHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IUserFile _userFile;
        private readonly string userPath;
        public CreateUserHandler(
            IConfiguration configuration,
            IUserFile userFile)
        {
            _userFile = userFile;
            userPath = configuration.GetSection("Paths").GetSection("UserFile").Value;
        }

        public Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = UserMapper.Mapper.Map<Sat.Recruitment.Core.Entities.User>(request);
            UserValidator rules = new UserValidator();
            ValidationResult validationResult = rules.Validate(entity);

            if (validationResult.IsValid)
            {
                var searchEntity = _userFile.GetDataUsersFile(userPath).Where(u => u.Equals(entity));
                if (!searchEntity.Any())
                {
                    IBonusStrategy strategy = BonusStrategyFactory.GetStrategy(entity.UserType);
                    if (strategy is null)
                    {
                        throw new BusinessException("User type is not valid");
                    }
                    entity.Money += strategy.CalculateBonus(entity);
                    int newUser = _userFile.WriteUsersFile(userPath, new[] { entity });
                    return Task.FromResult(Result.Success());
                } 
                else
                {
                    throw new BusinessException("User already exists");
                }
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }
}
