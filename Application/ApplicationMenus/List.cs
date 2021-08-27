using Application.Core;
using Domain;
using MediatR;
using Persistance;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ApplicationMenus
{
    public class List 
    {
        public class Query : IRequest<List<ApplicationMenuDto>>
        {
            public int UserId { get; set; }
            public int AppId { get; set; }
            public string Langunage { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<ApplicationMenuDto>>
        {
            private readonly IUnitOfWork unitOfWork;
            private readonly UserAccessor userAccessor;

            public Handler(IUnitOfWork unitOfWork,UserAccessor userAccessor)
            {
                this.unitOfWork = unitOfWork;
                this.userAccessor = userAccessor;
            }

            public async Task<List<ApplicationMenuDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var x = userAccessor.GetAuthToken();
                var y = userAccessor.GetUserId();

                var applicationMenus = await unitOfWork.ApplicationMenu.FindAll(request.UserId, request.AppId, request.Langunage);
                var rootMenus = applicationMenus.FindAll(x => x.ParentMenuId == null);

                List<ApplicationMenuDto> applicationMenuDtos = new();
                foreach(ApplicationMenu applicationMenu in rootMenus)
                {
                    applicationMenuDtos.Add(Initiate(applicationMenu, applicationMenus));
                }

                return applicationMenuDtos;
            }

            private ApplicationMenuDto Initiate(ApplicationMenu applicationMenu, List<ApplicationMenu> applicationMenus)
            {
                ApplicationMenuDto applicationMenuDto = new()
                {
                    Id = applicationMenu.Id,
                    Title = applicationMenu.Title,
                    Url = applicationMenu.Url,
                };
                applicationMenuDto.ChildMenus = new List<ApplicationMenuDto>();
                var children = applicationMenus.FindAll(x => x.ParentMenuId == applicationMenu.Id);
                foreach (ApplicationMenu menu in children)
                {
                    applicationMenuDto.ChildMenus.Add(Initiate(menu, applicationMenus));
                }

                return applicationMenuDto;
            }
        }
    }
}
