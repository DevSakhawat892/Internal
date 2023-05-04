using Microsoft.EntityFrameworkCore;
using TUSO.Domain.Entities;

/*
 * Created by: Labib, Bithy
 * Date created: 31.08.2022, 22.09.2022
 * Last modified: 22.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.SqlServer
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        /// <summary>
        /// Represents Countries entity.
        /// </summary>
        public DbSet<Country> Countries { get; set; }

        /// <summary>
        /// Represents Provinces entity.
        /// </summary>
        public DbSet<Province> Provinces { get; set; }

        /// <summary>
        /// Represents Districts entity.
        /// </summary>
        public DbSet<District> Districts { get; set; }

        /// <summary>
        /// Represents Facilities entity.
        /// </summary>
        public DbSet<Facility> Facilities { get; set; }

        /// <summary>
        /// Represents Roles entity.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Represents UserAccounts entity.
        /// </summary>
        public DbSet<UserAccount> UserAccounts { get; set; }

        /// <summary>
        /// Represents Modules entity.
        /// </summary>
        public DbSet<Module> Modules { get; set; }

        /// <summary>
        /// Represents IncidentTypes entity.
        /// </summary>
        public DbSet<IncidentType> IncidentTypes { get; set; }

        /// <summary>
        /// Represents Incidents entity.
        /// </summary>
        public DbSet<Incident> Incidents { get; set; }

        /// <summary>
        /// Represents IncidentStatuses entity.
        /// </summary>
        public DbSet<IncidentStatus> IncidentStatuses { get; set; }

        /// <summary>
        /// Represents Priorities entity.
        /// </summary>
        public DbSet<Priority> Priorities { get; set; }

        /// <summary>
        /// Represents SLAs entity.
        /// </summary>
        public DbSet<SLA> SLAs { get; set; }

        /// <summary>
        /// Represents ExclationRules entity.
        /// </summary>
        public DbSet<ExclationRule> ExclationRules { get; set; }

        /// <summary>
        /// Represents Holidays Holidays.
        /// </summary>
        public DbSet<Holiday> Holidays { get; set; }

        /// <summary>
        /// Represents HolidayLists Holidays.
        /// </summary>
        public DbSet<HolidayList> HolidayLists { get; set; }

        /// <summary>
        /// Represents Assignments entity.
        /// </summary>
        public DbSet<Assignment> Assignments { get; set; }

        /// <summary>
        /// Represents RecoveryRequests entity.
        /// </summary>
        public DbSet<RecoveryRequest> RecoveryRequests { get; set; }

        /// <summary>
        /// Represents Notifications entity.
        /// </summary>
        public DbSet<Notification> Notifications { get; set; }

        /// <summary>
        /// Represents ConfigureNotifications entity for many-to-many relation purpose.
        /// </summary>
        public DbSet<NotificationPermission> NotificationPermissions { get; set; }

        /// <summary>
        /// Represents Designation entity.
        /// </summary>
        public DbSet<Department> Departments { get; set; }

        /// <summary>
        /// Represents Designation entity.
        /// </summary>
        public DbSet<Designation> Designations { get; set; }

        /// <summary>
        /// Represents ApplicationPermissions entity.
        /// </summary>
        public DbSet<ApplicationPermission> ApplicationPermissions { get; set; }

        /// <summary>
        /// Represents GeographicPermission entity.
        /// </summary>
        public DbSet<GeographicPermission> GeographicPermissions { get; set; }

        /// <summary>
        /// Represents ProfilePicture entity.
        /// </summary>
        public DbSet<ProfilePicture> ProfilePictures { get; set; }

        /// <summary>
        /// Represents RouteTypes entity.
        /// </summary>
        public DbSet<RouteType> RouteTypes { get; set; }

        /// <summary>
        /// Represents TicketRoutings entity.
        /// </summary>
        public DbSet<TicketRouting> TicketRoutings { get; set; }

        /// <summary>
        /// Represents TierLevels entity.
        /// </summary>
        public DbSet<TierLevel> TierLevels { get; set; }
    }
}