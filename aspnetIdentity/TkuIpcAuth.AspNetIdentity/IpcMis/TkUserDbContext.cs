using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace TkuIpcAuth.AspNetIdentity {
    public class TkUserDbContext : DbContext {
        public TkUserDbContext() : base("name=IdentityDb") {}

        public TkUserDbContext(string connectionString) : base (connectionString) {}

        public DbSet<TkClaim> TkClaims { get; set; }
        public DbSet<TkRole> TkRoles { get; set; }
        public DbSet<TkUser> TkUsers { get; set; }
        public DbSet<TkUserLoginInfo> TkUserLoginInfos { get; set; }        
        public DbSet<TkUserRole> TkUserRoles { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            string schemaName = "bd";
            configTkClaim(modelBuilder.Entity<TkClaim>(), "vw_tkClaim", schemaName);
            configTkRole(modelBuilder.Entity<TkRole>(), "vw_tkRole", schemaName);
            configTkUser(modelBuilder.Entity<TkUser>(), "vw_tkUser", schemaName);
            configTkUserLoginInfo(modelBuilder.Entity<TkUserLoginInfo>(), "vw_tkUserLoginInfo", schemaName);
            configTkUserRole(modelBuilder.Entity<TkUserRole>(), "vw_tkUserRole", schemaName);
                        
            base.OnModelCreating(modelBuilder);
        }

        private void configTkClaim(EntityTypeConfiguration<TkClaim> config, string tableName, string schemaName = "") {
            config.ToTable(tableName, schemaName);
            config.HasKey(m => new { m.ClaimType, m.ClaimValue, m.UserId });
            
            config.Property(m => m.ClaimType).IsRequired().HasMaxLength(10).HasColumnType("char").HasColumnOrder(1);
            config.Property(m => m.ClaimValue).IsRequired().HasMaxLength(256).HasColumnType("varchar").HasColumnOrder(2);
            config.Property(m => m.UserId).IsRequired().HasMaxLength(10).HasColumnType("char").HasColumnOrder(3);
        }

        private void configTkRole(EntityTypeConfiguration<TkRole> config, string tableName, string schemaName = "") {
            config.ToTable(tableName, schemaName);
            config.HasKey(m => new { m.Id });
            
            config.Property(m => m.Id).IsRequired().HasMaxLength(12).HasColumnType("char").HasColumnOrder(1);
            config.Property(m => m.Name).IsRequired().HasMaxLength(10).HasColumnType("char").HasColumnOrder(2).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleIdIndex") { IsUnique = true }));
            config.Property(m => m.RoleName).HasMaxLength(40).HasColumnType("varchar").HasColumnOrder(3);
        }

        private void configTkUser(EntityTypeConfiguration<TkUser> config, string tableName, string schemaName = "") {             
            config.ToTable(tableName, schemaName);
            config.HasKey(m => m.Id);
            
            // 因為是使用檢視作為 model 來源，當設定外鍵關聯時，務必確定資料型態與長度一致。
            // 否則就算設定沒有錯誤 (virtual, 外鍵關聯、Sql Analyzer 也確定有篩出資料)，也無法將資料塞入導覽屬性
            config.HasMany(m => m.TkClaims).WithRequired().HasForeignKey(m => m.UserId).WillCascadeOnDelete();
            config.HasMany(m => m.TkUserLoginInfos).WithRequired().HasForeignKey(m => m.UserId).WillCascadeOnDelete();
            config.HasMany(m => m.TkUserRoles).WithRequired().HasForeignKey(m => m.UserId).WillCascadeOnDelete();

            config.Property(m => m.Id).IsRequired().HasColumnType("char").HasColumnOrder(1);//.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            config.Property(m => m.UserPswd).HasMaxLength(60).HasColumnType("varchar").HasColumnOrder(2);
            config.Property(m => m.UserName).HasMaxLength(6).IsRequired().HasColumnType("char").HasColumnOrder(3).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("EmplNoIndex") { IsUnique = true }));;
            config.Property(m => m.RealName).HasMaxLength(12).HasColumnType("varchar").HasColumnOrder(4);
            config.Property(m => m.FutmDept).HasMaxLength(5).HasColumnType("char").HasColumnOrder(5);
            config.Property(m => m.PasswordHash).HasMaxLength(500).HasColumnType("varchar").HasColumnOrder(6);
            config.Property(m => m.PhoneExt).HasMaxLength(20).HasColumnType("varchar").HasColumnOrder(7);
            config.Property(m => m.UserDept).HasMaxLength(128).HasColumnType("varchar").HasColumnOrder(8);
            config.Property(m => m.UserDeptInq).HasMaxLength(128).HasColumnType("varchar").HasColumnOrder(9);
        }

        private void configTkUserLoginInfo(EntityTypeConfiguration<TkUserLoginInfo> config, string tableName, string schemaName = "") {
            config.ToTable(tableName, schemaName);
            config.HasKey(m => new { m.LoginProvider, m.ProviderKey, m.UserId });
                        
            config.Property(m => m.LoginProvider).IsRequired().HasMaxLength(9).HasColumnType("char").HasColumnOrder(1);
            config.Property(m => m.ProviderKey).IsRequired().HasMaxLength(256).HasColumnType("varchar").HasColumnOrder(2);
            config.Property(m => m.UserId).IsRequired().HasMaxLength(10).HasColumnType("char").HasColumnOrder(3);
        }

        private void configTkUserRole(EntityTypeConfiguration<TkUserRole> config, string tableName, string schemaName = "") {
            config.ToTable(tableName, schemaName);
            config.HasKey(m => new { m.UserId, m.RoleId });

            config.Property(m => m.UserId).IsRequired().HasMaxLength(10).HasColumnType("char").HasColumnOrder(1);
            config.Property(m => m.RoleId).IsRequired().HasMaxLength(10).HasColumnType("char").HasColumnOrder(2);
        }
    }
}
