using Persistance.Interfaces;

namespace Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IApplicationMenuRepository applicationMenuRepository, IApplicationUserRepositoryDummy applicationUserRepository,
            ICityRepository cityRepository,ICompanyRepository companyRepository,ICostCenterRepository costCenterRepository,
            IDepartmentRepository departmentRepository,IDestinationRepository destinationRepository,IFloorRepository floorRepository,
            IIOTypeRepository iOTypeRepository,IOfficeRepository officeRepository,ISiteRepository siteRepository)
        {
            ApplicationMenu = applicationMenuRepository;
            ApplicationUser = applicationUserRepository;
            City = cityRepository;
            Company = companyRepository;
            CostCenter = costCenterRepository;
            Department = departmentRepository;
            Destination = destinationRepository;
            Floor = floorRepository;
            IOType = iOTypeRepository;
            Office = officeRepository;
            Site = siteRepository;
        }

        public IApplicationMenuRepository ApplicationMenu  { get; }
        public IApplicationUserRepositoryDummy ApplicationUser { get; }
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
