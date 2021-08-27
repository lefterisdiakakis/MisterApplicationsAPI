using Application.Core;
using Domain;
using MediatR;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class GetAll
    {
        public class Query : IRequest<Result<List<AbstractEntity>>>
        {
            public string Entity { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<List<AbstractEntity>>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<List<AbstractEntity>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string entity = request.Entity.ToLower();                
                // TODO: MIXALI FTIAKSTO
                switch (entity)
                {
                    case string x when x.Equals(nameof(City), StringComparison.InvariantCultureIgnoreCase):
                        var cities = await _unitOfWork.City.GetAllAsync();
                        return Result<List<AbstractEntity>>.Success(cities.ConvertAll(x => (AbstractEntity)x));
                    case string x when x.Equals(nameof(Company), StringComparison.InvariantCultureIgnoreCase):
                        var companies = await _unitOfWork.Company.GetAllAsync();
                        return Result<List<AbstractEntity>>.Success(companies.ConvertAll(x => (AbstractEntity)x));
                    case string x when x.Equals(nameof(CostCenter), StringComparison.InvariantCultureIgnoreCase):
                        var costCenters = await _unitOfWork.CostCenter.GetAllAsync();
                        return Result<List<AbstractEntity>>.Success(costCenters.ConvertAll(x => (AbstractEntity)x));
                        case string x when x.Equals(nameof(Department), StringComparison.InvariantCultureIgnoreCase):
                        var departments = await _unitOfWork.Department.GetAllAsync();
                        return Result<List<AbstractEntity>>.Success(departments.ConvertAll(x => (AbstractEntity)x));
                    case string x when x.Equals(nameof(Destination), StringComparison.InvariantCultureIgnoreCase):
                        var destinations = await _unitOfWork.Destination.GetAllAsync();
                        return Result<List<AbstractEntity>>.Success(destinations.ConvertAll(x => (AbstractEntity)x));
                    case string x when x.Equals(nameof(Floor), StringComparison.InvariantCultureIgnoreCase):
                        var floors = await _unitOfWork.Floor.GetAllAsync();
                        return Result<List<AbstractEntity>>.Success(floors.ConvertAll(x => (AbstractEntity)x));
                    case string x when x.Equals(nameof(IOType), StringComparison.InvariantCultureIgnoreCase):
                        var iotypes = await _unitOfWork.IOType.GetAllAsync();
                        return Result<List<AbstractEntity>>.Success(iotypes.ConvertAll(x => (AbstractEntity)x));
                    case string x when x.Equals(nameof(Office), StringComparison.InvariantCultureIgnoreCase):
                        var offices = await _unitOfWork.Office.GetAllAsync();
                        return Result<List<AbstractEntity>>.Success(offices.ConvertAll(x => (AbstractEntity)x));
                    case string x when x.Equals(nameof(Site), StringComparison.InvariantCultureIgnoreCase):
                        var sites = await _unitOfWork.Site.GetAllAsync();
                        return Result<List<AbstractEntity>>.Success(sites.ConvertAll(x => (AbstractEntity)x));
                    default:
                        throw new NotImplementedException();
                }


            }
        }
    }
}
