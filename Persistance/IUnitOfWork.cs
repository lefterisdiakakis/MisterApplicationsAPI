using Persistance.Interfaces;

namespace Persistance
{
    public interface IUnitOfWork
    {
        public IApplicationMenuRepository ApplicationMenu { get; }
        public IApplicationUserRepository ApplicationUser { get; }
        public ICityRepository City { get; }
        public ICompanyRepository Company { get; }
        public ICostCenterRepository CostCenter { get; }
        public IDepartmentRepository Department { get; }
        public IDestinationRepository Destination { get; }
        public IFloorRepository Floor { get; }
        public IIOTypeRepository IOType { get; }
        public IOfficeRepository Office { get; }
        public ISiteRepository Site { get; }
    }
}
