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
    public class AuthenticateSso
    {
        public class Command : IRequest<Result<bool>>
        {
            public LogInDto LogInDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                string Username = request.LogInDto.Username;
                string Password = request.LogInDto.Password;
                string Code_Challenge = request.LogInDto.Code_Challenge;

                var applicationUser = await _unitOfWork.ApplicationUser.GetUserAsync(Username, Password);

                if (applicationUser == null)
                    return Result<bool>.NotAuthorized("Error username/password");

                applicationUser.Code_Verifier = Code_Challenge;
                await _unitOfWork.ApplicationUser.UpdateUserAsync(applicationUser);
                return Result<bool>.Success(true);
            }
        }
    }
}
