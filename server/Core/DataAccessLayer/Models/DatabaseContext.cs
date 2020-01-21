using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppPermissions> AppPermissions { get; set; }
        public virtual DbSet<AppRolePermissions> AppRolePermissions { get; set; }
        public virtual DbSet<AppRoles> AppRoles { get; set; }
        public virtual DbSet<AppUserRoles> AppUserRoles { get; set; }
        public virtual DbSet<AppUsers> AppUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress2014;Database=Database;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<AppPermissions>(entity =>
            {
                entity.HasIndex(e => e.PermissionName)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PermissionName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AppRolePermissions>(entity =>
            {
                entity.HasIndex(e => new { e.PermissionId, e.RoleId })
                    .HasName("PK_AppRolePermissions")
                    .IsUnique();

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.AppRolePermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppRolePermissions_AppPermissions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AppRolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppRolePermissions_AppRoles");
            });

            modelBuilder.Entity<AppRoles>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AppUserRoles>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.RoleId })
                    .HasName("PK_UserRole")
                    .IsUnique();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AppUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppUserRoles_AppRoles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AppUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppUserRoles_AppUsers");
            });

            modelBuilder.Entity<AppUsers>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Avartar).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.MidName).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255);
            });
        }
    }
}
