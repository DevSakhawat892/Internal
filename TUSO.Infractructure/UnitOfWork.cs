using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.Repositories;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Labib, Sakhawat, Bithy
 * Date created: 31.08.2022, 04.09.2022, 10.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure
{
   public class UnitOfWork : IUnitOfWork
   {
      protected readonly DataContext context;
      public UnitOfWork(DataContext context)
      {
         this.context = context;
      }

      public async Task<int> SaveChangesAsync()
      {
         return await context.SaveChangesAsync();
      }

      #region CountryRepository
      private ICountryRepository countryRepository;
      public ICountryRepository CountryRepository
      {
         get
         {
            if (countryRepository == null)
               countryRepository = new CountryRepository(context);

            return countryRepository;
         }
      }
      #endregion

      #region ProvinceRepository
      private IProvinceRepository provinceRepository;
      public IProvinceRepository ProvinceRepository
      {
         get
         {
            if (provinceRepository == null)
               provinceRepository = new ProvinceRepository(context);

            return provinceRepository;
         }
      }
      #endregion

      #region DistrictRepository 
      private IDistrictRepository districtRepository;
      public IDistrictRepository DistrictRepository
      {
         get
         {
            if (districtRepository == null)
               districtRepository = new DistrictRepository(context);

            return districtRepository;
         }
      }
      #endregion

      #region Facility
      private IFacilityRepository facilityRepository;
      public IFacilityRepository FacilityRepository
      {
         get
         {
            if (facilityRepository == null)
               facilityRepository = new FacilityRepository(context);

            return facilityRepository;
         }
      }
      #endregion

      #region RoleRepository
      private IRoleRepository roleRepository;
      public IRoleRepository RoleRepository
      {
         get
         {
            if (roleRepository == null)
               roleRepository = new RoleRepository(context);
            return roleRepository;
         }
      }
      #endregion

      #region UserAccountRepository
      private IUserAccountRepository userAccountRepository;
      public IUserAccountRepository UserAccountRepository
      {
         get
         {
            if (userAccountRepository == null)
               userAccountRepository = new UserAccountRepository(context);

            return userAccountRepository;
         }
      }
      #endregion

      #region ProfilePicture
      IProfilePictureRepository profilePictureRepository;
      public IProfilePictureRepository ProfilePictureRepository
      {
         get
         {
            if (profilePictureRepository == null)
               profilePictureRepository = new ProfilePictureRepository(context);

            return profilePictureRepository;
         }
      }
      #endregion

      #region ModuleRepository
      private IModuleRepository moduleRepository;
      public IModuleRepository ModuleRepository
      {
         get
         {
            if (moduleRepository == null)
               moduleRepository = new ModuleRepository(context);
            return moduleRepository;
         }
      }
      #endregion

      #region ProfilePictureRepository
      //private IProfilePictureRepository profilePictureRepository;
      //public IProfilePictureRepository ProfilePictureRepository
      //{
      //    get
      //    {
      //        if (profilePictureRepository == null)
      //            profilePictureRepository = new ProfilePictureRepository(context);
      //        return profilePictureRepository;
      //    }
      //}
      #endregion

      #region IncidentTypeRepository
      private IIncidentTypeRepository incidentTypeRepository;
      public IIncidentTypeRepository IncidentTypeRepository
      {
         get
         {
            if (incidentTypeRepository == null)
               incidentTypeRepository = new IncidentTypeRepository(context);
            return incidentTypeRepository;
         }
      }
      #endregion

      #region ApplicationPermissionRepository
      //private IApplicationPermissionRepository applicationPermissionRepository;
      //public IApplicationPermissionRepository ApplicationPermissionRepository
      //{
      //    get
      //    {
      //        if (applicationPermissionRepository == null)
      //            applicationPermissionRepository = new ApplicationPermissionRepository(context);
      //        return applicationPermissionRepository;
      //    }
      //}
      #endregion

      #region PreviewRepository
      private IPreviewRepository previewRepository;
      public IPreviewRepository PreviewRepository
      {
         get
         {
            if (previewRepository == null)
               previewRepository = new PreviewRepository(context);
            return previewRepository;
         }
      }
      #endregion

      #region AssignmentRepository
      private IAssignmentRepository assignmentRepository;
      public IAssignmentRepository AssignmentRepository
      {
         get
         {
            if (assignmentRepository == null)
               assignmentRepository = new AssignmentRepository(context);
            return assignmentRepository;
         }
      }
      #endregion

      #region IIncidentRepository
      private IIncidentRepository incidentRepository;
      public IIncidentRepository IncidentRepository
      {
         get
         {
            if (incidentRepository == null)
               incidentRepository = new IncidentRepository(context);
            return incidentRepository;
         }
      }
      #endregion

      #region IIncidentStatusRepository
      private IIncidentStatusRepository incidentStatusRepository;
      public IIncidentStatusRepository IncidentStatusRepository
      {
         get
         {
            if (incidentStatusRepository == null)
               incidentStatusRepository = new IncidentStatusRepository(context);
            return incidentStatusRepository;
         }
      }
      #endregion

      #region GeographicPermissionRepository
      private IGeographicPermissionRepository geographicPermissionRepository;
      public IGeographicPermissionRepository GeographicPermissionRepository
      {
         get
         {
            if (geographicPermissionRepository == null)
               geographicPermissionRepository = new GeographicPermissionRepository(context);
            return geographicPermissionRepository;
         }
      }
      #endregion

      #region IncidentPermissionRepository
      //private IIncidentPermissionRepository incidentPermissionRepository;
      //public IIncidentPermissionRepository IncidentPermissionRepository
      //{
      //    get
      //    {
      //        if (incidentPermissionRepository == null)
      //            incidentPermissionRepository = new IncidentPermissionRepository(context);
      //        return incidentPermissionRepository;
      //    }
      //}
      #endregion

      #region NotificationRepository
      private INotificationRepository notificationRepository;
      public INotificationRepository NotificationRepository
      {
         get
         {
            if (notificationRepository == null)
               notificationRepository = new NotificationRepository(context);
            return notificationRepository;
         }
      }
      #endregion

      #region ConfigureNotificationRepository
      private INotificationPermissionRepository notificationPermissionRepository;
      public INotificationPermissionRepository NotificationPermissionRepository
      {
         get
         {
            if (notificationPermissionRepository == null)
               notificationPermissionRepository = new NotificationPermissionRepository(context);
            return notificationPermissionRepository;
         }
      }
      #endregion

      #region RecoveryRequestRepository
      IRecoveryRequestRepository recoveryRequestRepository;
      public IRecoveryRequestRepository RecoveryRequestRepository
      {
         get
         {
            if (recoveryRequestRepository == null)
               recoveryRequestRepository = new RecoveryRequestRepository(context);

            return recoveryRequestRepository;
         }
      }
      #endregion

      #region Department
      IDepartmentRepository departmentRepository;
      public IDepartmentRepository DepartmentRepository
      {
         get
         {
            if (departmentRepository == null)
               departmentRepository = new DepartmentRepository(context);

            return departmentRepository;
         }
      }
      #endregion

      #region Designation
      IDesignationRepository designationRepository;
      public IDesignationRepository DesignationRepository
      {
         get
         {
            if (designationRepository == null)
               designationRepository = new DesignationRepository(context);

            return designationRepository;
         }
      }
      #endregion

      #region Incident
      IIncidentCategoryRepository incidentCategoryRepository;
      public IIncidentCategoryRepository IncidentCategoryRepository
      {
         get
         {
            if (incidentCategoryRepository == null)
               incidentCategoryRepository = new IncidentCategoryRepository(context);

            return incidentCategoryRepository;
         }
      }
      #endregion

      #region Priority
      IPriorityRepository priorityRepository;
      public IPriorityRepository PriorityRepository
      {
         get
         {
            if (priorityRepository == null)
               priorityRepository = new PriorityRepository(context);

            return priorityRepository;
         }
      }
      #endregion

      #region ExclationRule
      IExclationRuleRepository exclationRuleRepository;
      public IExclationRuleRepository ExclationRuleRepository
      {
         get
         {
            if (exclationRuleRepository == null)
               exclationRuleRepository = new ExclationRuleRepository(context);

            return exclationRuleRepository;
         }
      }
      #endregion

      #region ApplicationPermission
      IApplicationPermissionRepository applicationPermissionRepository;
      public IApplicationPermissionRepository ApplicationPermissionRepository
      {
         get
         {
            if (applicationPermissionRepository == null)
               applicationPermissionRepository = new ApplicationPermissionRepository(context);

            return applicationPermissionRepository;
         }
      }
      #endregion

      #region HolidayRepository
      IHolidayRepository holidayRepository;
      public IHolidayRepository HolidayRepository
      {
         get
         {
            if (holidayRepository == null)
               holidayRepository = new HolidayRepository(context);

            return holidayRepository;
         }
      }
      #endregion

      #region HolidayListRepository
      IHolidayListRepository holidayListRepository;
      public IHolidayListRepository HolidayListRepository
      {
         get
         {
            if (holidayListRepository == null)
               holidayListRepository = new HolidayListRepository(context);

            return holidayListRepository;
         }
      }
      #endregion

      #region SLA
      ISLARepository sLARepository;
      public ISLARepository SLARepository
      {
         get
         {
            if (sLARepository == null)
               sLARepository = new SLARepository(context);

            return sLARepository;
         }
      }
      #endregion

      #region TierLevel
      ITierLevelRepository tierLevelRepository;
      public ITierLevelRepository TierLevelRepository
      {
         get
         {
            if (tierLevelRepository == null)
               tierLevelRepository = new TierLevelRepository(context);

            return tierLevelRepository;
         }
      }
      #endregion

      #region RouteType
      IRouteTypeRepository routeTypeRepository;
      public IRouteTypeRepository RouteTypeRepository
      {
         get
         {
            if (routeTypeRepository == null)
               routeTypeRepository = new RouteTypeRepository(context);

            return routeTypeRepository;
         }
      }
      #endregion

      #region TicketRouting
      ITicketRoutingRepository ticketRoutingRepository;
      public ITicketRoutingRepository TicketRoutingRepository
      {
         get
         {
            if (ticketRoutingRepository == null)
               ticketRoutingRepository = new TicketRoutingRepository(context);

            return ticketRoutingRepository;
         }
      }
      #endregion
   }
}