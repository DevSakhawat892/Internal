/*
 * Created by: Labib, Sakhawat, Bithy
 * Date created: 31.08.2022, 04.09.2022, 20.09.2022
 * Last modified: 01.09.2022, 20.09.2022
 * Modified by: Sakhawat, Bithy
 */

namespace TUSO.Utilities.Constants
{
   public static class RouteConstants
   {
      public const string BaseRoute = "tuso-api";

      #region Country
      public const string CreateCountry = "country";

      public const string ReadCountries = "countries";

      public const string ReadCountryByKey = "country/key/{key}";

      public const string UpdateCountry = "country/{key}";

      public const string DeleteCountry = "country/{key}";
      #endregion

      #region Province
      public const string CreateProvince = "province";

      public const string ReadProvinces = "provinces";

      public const string ReadProvinceByKey = "province/key/{key}";

      public const string ReadProvinceByCountry = "province/country/{CountryID}";

      public const string UpdateProvince = "province/{key}";

      public const string DeleteProvince = "province/{key}";
      #endregion

      #region Facility 
      public const string CreateFacility = "facility";

      public const string ReadFacilities = "facilities";

      public const string ReadFacilityByKey = "facility/key/{OID}";

      public const string ReadFacilityByDistrict = "facility/district/{DistrictID}";

      public const string UpdateFacility = "facility/{key}";

      public const string DeleteFacility = "facility/{key}";
      #endregion

      #region District
      public const string CreateDistrict = "district";

      public const string ReadDistrict = "districts";

      public const string ReadDistrictByKey = "district/key/{key}";

      public const string ReadDistrictByProvince = "district/province/{ProvinceID}";

      public const string UpdateDistrict = "district/{key}";

      public const string DeleteDistrict = "district/{key}";
      #endregion

      #region Roles
      public const string CreateUserRole = "user-role";

      public const string ReadUserRoles = "user-roles";

      public const string ReadUserRoleByKey = "user-role/key/{key}";

      public const string UpdateUserRole = "user-role/{key}";

      public const string DeleteUserRole = "user-role/{key}";
      #endregion

      #region UserAccounts
      public const string CreateUserAccount = "user-account";

      public const string ReadUserAccounts = "user-accounts";

      public const string ReadUserAccountByKey = "user-account/key/{key}";

      public const string ReadUserAccountByUsername = "user-account/{username}";

      public const string UserDetails = "user-account/details/{key}";

      public const string UpdateUserAccount = "user-account/{key}";

      public const string DeleteUserAccount = "user-account/{key}";

      public const string UserLogin = "user-account/login";

      public const string ChangedPassword = "user-account/change-password";

      public const string RecoveryRequest = "user-account/recovery-request";
      #endregion

      #region Modules
      public const string CreateModule = "module";

      public const string ReadModules = "modules";

      public const string ReadModuleByKey = "module/key/{key}";

      public const string UpdateModule = "module/{key}";

      public const string DeleteModule = "module/{key}";
      #endregion

      #region IncidentType
      public const string CreateIncidentType = "incident-type";

      public const string ReadIncidentTypes = "incident-types";

      public const string ReadIncidentTypeByKey = "incident-type/key/{key}";

      public const string UpdateIncidentType = "incident-type/{key}";

      public const string DeleteIncidentType = "incident-type/{key}";
      #endregion

      #region Incident
      public const string CreateIncident = "incident";

      public const string ReadIncidents = "incidents";

      public const string ReadIncidentByKey = "incident/key/{key}";

      public const string UpdateIncident = "incident/{key}";

      public const string DeleteIncident = "incident/{key}";
      #endregion

      #region IncidentStatus
      public const string CreateIncidentStatus = "incident-status";

      public const string ReadIncidentStatuses = "incident-statuses";

      public const string ReadIncidentStatusByKey = "incident-status/key/{key}";

      public const string UpdateIncidentStatus = "incident-status/{key}";

      public const string DeleteIncidentStatus = "incident-status/{key}";
      #endregion

      #region Permission
      public const string CreatePermission = "permission";

      public const string ReadPermissions = "permissions";

      public const string ReadPermissionByKey = "permission/key/{key}";

      public const string UpdatePermission = "permission/{key}";

      public const string DeletePermission = "permission/{key}";
      #endregion

      #region Preview
      public const string CreatePreview = "preview";

      public const string ReadPreviews = "previews";

      public const string ReadPreviewByKey = "preview/key/{key}";

      public const string UpdatePreview = "preview/{key}";

      public const string DeletePreview = "preview/{key}";
      #endregion

      #region Assignment
      public const string CreateAssignment = "assignment";

      public const string ReadAssignments = "assignments";

      public const string ReadAssignmentByKey = "assignment/key/{key}";

      public const string UpdateAssignment = "assignment/{key}";

      public const string DeleteAssignment = "assignment/{key}";
      #endregion

      #region Notification

      public const string ReadNotifications = "notifications";

      public const string ReadNotificationByKey = "notification/key/{key}";

      public const string CreateNotification = "notification";

      public const string DeleteNotification = "notification/key";

      public const string MarkAllAsRead = "notification/key/{key}";

      public const string MarkAsRead = "notification/key/{notid}";
      #endregion

      #region RecoveryRequest

      public const string CreateRecoveryRequest = "recovery-request";

      public const string ReadRecoveryRequests = "recovery-requests";

      public const string ReadRecoveryRequestByKey = "recovery-request/key/{key}";

      public const string UpdateRecoveryRequest = "recovery-request/key";

      //public const string RequestPasswordRecovery = "request-password-recovery";

      public const string DeleteRecoveryRequest = "recovery-request/key";
      #endregion

      #region Project
      public const string CreateProject = "project";

      public const string ReadProjects = "projects";
      /// <summary>
      /// OID is used as primary key of project entity.
      /// </summary>

      public const string ReadProjectByKey = "project/key/{OID}";

      public const string UpdateProject = "project/{key}";

      public const string DeleteProject = "project/{key}";
      #endregion

      #region ProfilePicture
      public const string CreateProfilePicture = "profile-picture";

      public const string ReadProfilePictures = "profile-pictures";

      public const string ReadProfilePictureByKey = "profile-picture/key/{OID}";
        
      public const string ReadProfilePictureByUser = "profile-picture/user/{UserID}"; 

      public const string UpdateProfilePicture = "profile-picture/{key}";

      public const string DeleteProfilePicture = "profile-picture/{key}";
      #endregion

      #region ApplicationPermission
      public const string CreateApplicationPermission = "application-permission";

      public const string ReadApplicationPermissions = "application-permissions";

      public const string ReadApplicationPermissionByKey = "application-permission/key/{OID}";

      public const string ReadApplicationPermissionByRole = "application-permission/role/{RoleID}";

      public const string ReadApplicationPermissionByModule = "application-permission/module/{ModuleID}";

      public const string ReadApplicationPermission = "application-permission/key";

      public const string UpdateApplicationPermission = "application-permission/{key}";

      public const string DeleteApplicationPermission = "application-permission/{key}";
      #endregion

      #region GeographicPermission
      public const string CreateGeographicPermission = "geographic-permission";

      public const string ReadGeographicPermissions = "geographic-permissions";

      public const string ReadGeographicPermissionByKey = "geographic-permission/key/{key}";

      public const string ReadGeographicPermissionByUser = "geographic-permission/useraccount/{UserAccountID}";

      public const string ReadGeographicPermissionByProvince = "geographic-permission/province/{ProvinceID}";

      public const string UpdateGeographicPermission = "geographic-permission/{key}";

      public const string DeleteGeographicPermission = "geographic-permission/{key}";
      #endregion

      #region IncidentPermission
      public const string CreateIncidentPermission = "incident-permission";

      public const string ReadIncidentPermissions = "incident-permissions";

      public const string ReadIncidentPermissionByKey = "incident-permission/key/{OID}";

      public const string ReadIncidentPermissionByRole = "incident-permission/role/{RoleID}";

      public const string ReadIncidentPermissionByIncidentType = "incident-permission/incidenttype/{IncidentTypeID}";

      public const string ReadIncidentPermission = "incident-permission/key";

      public const string UpdateIncidentPermission = "incident-permission/{key}";

      public const string DeleteIncidentPermission = "incident-permission/{key}";
      #endregion

      #region ProjectPermission
      public const string CreateProjectPermission = "project-permission";

      public const string ReadProjectPermissions = "project-permissions";

      public const string ReadProjectPermissionByKey = "project-permission/key/{OID}";

      public const string ReadProjectPermissionByRole = "project-permission/role/{RoleID}";

      public const string ReadProjectPermissionByProject = "project-permission/project/{ProjectID}";

      public const string ReadProjectPermission = "project-permission/key";

      public const string UpdateProjectPermission = "project-permission/{key}";

      public const string DeleteProjectPermission = "project-permission/{key}";
      #endregion

      #region Department
      public const string CreateDepartment = "department";

      public const string ReadDepartments = "departments";

      public const string ReadDepartmentByKey = "department/key/{key}";

      public const string UpdateDepartment = "department/{key}";

      public const string DeleteDepartment = "department/{key}";
      #endregion

      #region Designation
      public const string CreateDesignation = "designation";

      public const string ReadDesignations = "designations";

      public const string ReadDesignationByKey = "designation/key/{OID}";

      public const string ReadDesignationByDepartment = "designation/department/{DepartmentID}";

      public const string UpdateDesignation = "designation/{key}";

      public const string DeleteDesignation = "designation/{key}";
      #endregion

      #region IncidentCategory
      public const string CreateIncidentCategory = "incident-category";

      public const string ReadIncidentCategories = "incident-categories";

      public const string ReadIncidentCategoryByKey = "incident-category/key/{OID}";

      public const string UpdateIncidentCategory = "incident-category/{key}";

      public const string DeleteIncidentCategory = "incident-category/{key}";
      #endregion

      #region Priority
      public const string CreatePriority = "prioritoy";

      public const string ReadPriorities = "priorities";

      public const string ReadPriorityByKey = "priority/key/{key}";

      public const string UpdatePriority = "priority/{key}";

      public const string DeletePriority = "priority/{key}";
      #endregion

      #region Holiday
      public const string CreateHoliday = "holiday";

      public const string ReadHolidays = "holidays";

      public const string ReadHolidayByKey = "holiday/key/{key}";

      public const string UpdateHoliday = "holiday/{key}";

      public const string DeleteHoliday = "holiday/{key}";
      #endregion

      #region HolidayList
      public const string CreateHolidayList = "holidaylist";

      public const string CreateAllHolidayList = "holidaylist-post";

      public const string CreateVacation = "holidaylist-vacation";

      public const string ReadHolidayLists = "holidaylists";

      public const string ReadHolidayListByKey = "holidaylist/key/{key}";

      public const string ReadHolidayListByHoliday = "holidaylist/holiday/{HolidayID}";

      public const string UpdateHolidayList = "holidaylist/{key}";

      public const string DeleteHolidayList = "holidaylist/{key}";
      #endregion

      #region ExclationRule
      public const string CreateExclationRule = "exclation-rule";

      public const string ReadExclationRules = "exclation-rules";

      public const string ReadExclationRuleByKey = "exclation-rule/key/{key}";

      public const string UpdateExclationRule = "exclation-rule/{key}";

      public const string DeleteExclationRule = "exclation-rule/{key}";
      #endregion

      #region SLA
      public const string CreateSLA = "sla";

      public const string ReadSLAs = "slas";

      public const string ReadSLAByKey = "sla/key/{OID}";

      public const string UpdateSLA = "sla/{key}";

      public const string DeleteSLA = "sla/{key}";
      #endregion

      #region TierLevel
      public const string CreateTierLevel = "tier-level";

      public const string ReadTierLevels = "tier-levels";

      public const string ReadTierLevelByKey = "tier-level/key/{OID}";

      public const string UpdateTierLevel = "tier-level/{key}";

      public const string DeleteTierLevel = "tier-level/{key}";
      #endregion

      #region RouteType
      public const string CreateRouteType = "route-type";

      public const string ReadRouteTypes = "route-types";

      public const string ReadRouteTypeByKey = "route-type/key/{key}";

      public const string UpdateRouteType = "route-type/{key}";

      public const string DeleteRouteType = "route-type/{key}";
      #endregion

      #region TicketRouting
      public const string CreateTicketRouting = "ticket-routing";

      public const string ReadTicketRoutings = "ticket-routings";

      public const string ReadTicketRoutingByKey = "ticket-routing/key/{key}";

      public const string UpdateTicketRouting = "ticket-routing/{key}";

      public const string DeleteTicketRouting = "ticket-routing/{key}";
      #endregion
   }
}