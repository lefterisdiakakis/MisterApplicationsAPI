using Application.Core;
using Domain;
using MediatR;
using Persistance;
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
                var applicationUser = await _unitOfWork.ApplicationUser.GetApplicationUserByUsername(Username);
                if (applicationUser == null)
                    return Result<UserDto>.NotAuthorized("User not found");

                if (applicationUser.Deleted)
                    return Result<UserDto>.NotAuthorized("User is deleted");

                if (!applicationUser.Active || !applicationUser.Visible)
                    return Result<UserDto>.NotAuthorized("User is disabled");


                switch (applicationUser.ResourceID)
                {
                    case (int)Resources.ActiveDirectory:
                        if(!_unitOfWork.ApplicationUser.AuthenticateViaLDAP(Username,Password))
                            return Result<UserDto>.NotAuthorized("Wrong Password");
                        break;
                    case (int)Resources.WebInterface:
                    case (int)Resources.SystemServices:
                    case (int)Resources.IpTelephonyUsers:
                    case (int)Resources.CUCMIntegration:
                    default:
                        string encryptedPassword = EncryptionService.EncryptString(Password);
                        if(encryptedPassword!=applicationUser.Password)
                            return Result<UserDto>.NotAuthorized("Wrong Password");
                        break;
                }

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
