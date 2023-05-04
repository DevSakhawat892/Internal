/*
 * Created by: Labib, Sakhawat, Bithy
 * Date created: 31.08.2022, 04.09.2022, 20.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */

namespace TUSO.Infrastructure.Contracts
{
    public interface IUnitOfWork
    {
        ICountryRepository CountryRepository { get; }

        IFacilityRepository FacilityRepository { get; }

        IProvinceRepository ProvinceRepository { get; }

        IDistrictRepository DistrictRepository { get; }

        IRoleRepository RoleRepository { get; }

        IUserAccountRepository UserAccountRepository { get; }

        IModuleRepository ModuleRepository { get; }

        IIncidentTypeRepository IncidentTypeRepository { get; }

        IIncidentRepository IncidentRepository { get; }

        IIncidentStatusRepository IncidentStatusRepository { get; }

        IPreviewRepository PreviewRepository { get; }

        IAssignmentRepository AssignmentRepository { get; }

        INotificationRepository NotificationRepository { get; }

        INotificationPermissionRepository NotificationPermissionRepository { get; }

        IApplicationPermissionRepository ApplicationPermissionRepository { get; }

        IGeographicPermissionRepository GeographicPermissionRepository { get; }

        IProfilePictureRepository ProfilePictureRepository { get; }

        IRecoveryRequestRepository RecoveryRequestRepository { get; }

        IDepartmentRepository DepartmentRepository { get; }

        IDesignationRepository DesignationRepository { get; }

        IIncidentCategoryRepository IncidentCategoryRepository { get; }

        IPriorityRepository PriorityRepository { get; }

        IExclationRuleRepository ExclationRuleRepository { get; }

        IHolidayRepository HolidayRepository { get; }

        IHolidayListRepository HolidayListRepository { get; }

        ISLARepository SLARepository { get; }

        ITierLevelRepository TierLevelRepository { get; }

        IRouteTypeRepository RouteTypeRepository { get; }

        ITicketRoutingRepository TicketRoutingRepository { get; }

        Task<int> SaveChangesAsync();
   }
}