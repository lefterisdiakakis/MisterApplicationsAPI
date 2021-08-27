using Application.Core;
using MediatR;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class Authenticate
    {
        public class Command : IRequest<Result<UserDto>>
        {
            public LogInDto LogInDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<UserDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly TokenService _tokenService;

            public Handler(IUnitOfWork unitOfWork, TokenService tokenService)
            {
                _unitOfWork = unitOfWork;
                _tokenService = tokenService;
            }

            public async Task<Result<UserDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                string Username = request.LogInDto.Username;
                string Password = request.LogInDto.Password;
                var applicationUser = await _unitOfWork.ApplicationUser.GetUserAsync(Username, Password);

                if (applicationUser == null)
                    return Result<UserDto>.NotAuthorized("Error username/password");

                var userDto = new UserDto
                {
                    Id = applicationUser.Id,
                    Username = applicationUser.Username,
                    Token = _tokenService.CreateToken(applicationUser)
                };

                return Result<UserDto>.Success(userDto);
            }
        }
    }
}
