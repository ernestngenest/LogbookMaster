using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Common.EntityFramework.Models;
using Kalbe.Library.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;
using Constant = Kalbe.Library.Common.Models.Constant;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Models
{
    public class InternsipLogbookMasterDataDataContext : BaseDbContext<InternsipLogbookMasterDataDataContext>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public InternsipLogbookMasterDataDataContext(DbContextOptions<InternsipLogbookMasterDataDataContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options, httpContextAccessor)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                modelBuilder.Entity<Role>()
                    .HasIndex(x => new { x.RoleCode, x.RoleName })
                    .IsUnique()
                    .HasFilter("\"IsDeleted\" = False");
                modelBuilder.Entity<School>()
                    .HasIndex(x => new { x.SchoolCode, x.SchoolName })
                    .IsUnique()
                    .HasFilter("\"IsDeleted\" = False");
                modelBuilder.Entity<Faculty>()
                    .HasIndex(x => new { x.FacultyCode, x.FacultyName })
                    .IsUnique()
                    .HasFilter("\"IsDeleted\" = False");
                modelBuilder.HasPostgresExtension("citext");
            }
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("citext");
        }
        public override void SetDefaultValues()
        {
            IEnumerable<EntityEntry> enumerable = from x in ChangeTracker.Entries()
                                                  where x.Entity is Base && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted)
                                                  select x;
            string text = Constant.DefaultActor;
            string text2 = Constant.DefaultActor;
            if (_httpContextAccessor.HttpContext != null)
            {
                ClaimsPrincipal user = _httpContextAccessor.HttpContext!.User;
                Claim claim = user.FindFirst((Claim x) => x.Type == "UserPrincipalName");
                Claim claim2 = user.FindFirst((Claim x) => x.Type == Constant.Name);
                if (claim != null)
                {
                    text = claim.Value;
                }

                if (claim2 != null)
                {
                    text2 = claim2.Value;
                }
            }

            foreach (EntityEntry item in enumerable)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        ((Base)item.Entity).CreatedDate = DateTime.Now;
                        ((Base)item.Entity).CreatedBy = text;
                        ((Base)item.Entity).CreatedByName = text2;
                        ((Base)item.Entity).IsDeleted = false;
                        break;
                    case EntityState.Modified:
                        ((Base)item.Entity).UpdatedDate = DateTime.Now;
                        ((Base)item.Entity).UpdatedBy = text;
                        ((Base)item.Entity).UpdatedByName = text2;
                        ((Base)item.Entity).IsDeleted = false;
                        break;
                    case EntityState.Deleted:
                        item.State = EntityState.Modified;
                        ((Base)item.Entity).UpdatedDate = DateTime.Now;
                        ((Base)item.Entity).UpdatedBy = text;
                        ((Base)item.Entity).UpdatedByName = text2;
                        ((Base)item.Entity).IsDeleted = true;
                        break;
                }
            }
        }

        //public virtual DbSet<Models.InternsipLogbookMasterData> InternsipLogbookMasterDatas { get; set; }
        public virtual DbSet<UserExternal> UserExternals { get; set; }
        public virtual DbSet<Logger> Loggers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<School> Schools { get; set;}
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Allowance> Allowances { get; set; }
        public virtual DbSet<Approval> Approvals { get; set; }
        public virtual DbSet<ApprovalDetail> ApprovalsDetails { get; set;}
        public virtual DbSet<UserInternal> UserInternals { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
    }
}
