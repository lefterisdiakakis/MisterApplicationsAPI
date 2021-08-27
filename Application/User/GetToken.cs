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
    public class GetToken
    {
        public class Query : IRequest<UserDto>
        {
            public string Code_Verifier { get; set; }
        }

        public class Handler : IRequestHandler<Query, UserDto>
        {
            private readonly IUnitOfWork unitOfWork;
            private readonly EncryptionService encryptionService;
            private readonly TokenService tokenService;

            public Handler(IUnitOfWork unitOfWork,EncryptionService encryptionService,TokenService tokenService)
            {
                this.unitOfWork = unitOfWork;
                this.encryptionService = encryptionService;
                this.tokenService = tokenService;
            }

            public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var applicationUser = await unitOfWork.ApplicationUser.GetByCodeVerifier(encryptionService.ComputeSha256Hash(request.Code_Verifier));
                if (applicationUser == null)
                    return null;

                UserDto userDto = new UserDto { Id = applicationUser.Id, Username = applicationUser.Username, Token = tokenService.CreateToken(applicationUser) };
                return userDto;
            }
        }
    }
}
