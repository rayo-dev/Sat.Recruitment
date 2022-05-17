using FluentValidation.Results;
using MediatR;
using Sat.Recruitment.Application.Common.Exceptions;
using Sat.Recruitment.Application.Common.Mappings;
using Sat.Recruitment.Application.Responses;
using Sat.Recruitment.Application.Users.Commands;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Strategies;
using Sat.Recruitment.Core.Validators;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Handlers.CommandHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserResponse>
    {
        private readonly IUserRepository _userRepo;

        public CreateUserHandler(
            IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = UserMapper.Mapper.Map<Sat.Recruitment.Core.Entities.User>(request);
            UserValidator rules = new UserValidator();
            ValidationResult validationResult = rules.Validate(entity);

            if (validationResult.IsValid)
            {
                var searchEntity = await _userRepo.Search(entity);
                if (!searchEntity.Any())
                {
                    IBonusStrategy strategy = BonusStrategyFactory.GetStrategy(entity.UserType);
                    if (strategy is null)
                    {
                        throw new NotFoundException("User type", entity.UserType);
                    }
                    entity.Money += strategy.CalculateBonus(entity);
                    var newUser = await _userRepo.AddAsync(entity);
                    return UserMapper.Mapper.Map<UserResponse>(newUser);
                } 
                else
                {
                    throw new BusinessException("User duplicaded");
                }
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }
}
