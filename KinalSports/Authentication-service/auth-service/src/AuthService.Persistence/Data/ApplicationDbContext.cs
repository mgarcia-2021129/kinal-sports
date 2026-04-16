using System;
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
namespace AuthService.Persistence.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users {get; set;}

    public DbSet<Role> Roles {get; set;}

    public DbSet<UserRole> UserRoles {get; set;}

    public DbSet<UserProfile> UserProfiles {get; set;}

    public DbSet<UserEmail> UserEmails {get; set;}

    public DbSet<UserPasswordReset> UserPasswordResets {get; set;}

    //Renombrar la entidad usando snake_case
    public static string ToSnakeCase(string input)
    {
        if(string.IsNullOrEmpty(input))
            return input;
        
        return string.Concat(
            input.Select((c, i) => i > 0 && char.IsUpper(c) ? "_" + c : c.ToString())
            ).ToLower();
    }
    //Sobrecarga el metodo que nos va permitir conectarnos a la base de datos.
    //Metodo que fuerza convencion global en tablas, columnas, llaves etc a usar snake_case.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); //Esto hace una herencia a la clase padre, y le pasa los parametos necsarios al metodo

        foreach(var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            //Snake Case para nombre de las tablas
            if(!string.IsNullOrEmpty(tableName))
            {
                entity.SetTableName(ToSnakeCase(tableName));
            }
            //Snake Case para nombre de las columnas
            foreach(var property in entity.GetProperties())
            {
                var columnName = property.GetColumnName();
                if(!string.IsNullOrEmpty(columnName))
                {
                    property.SetColumnName(ToSnakeCase(columnName));
                }
            }
            //Foreing Key con snake case y Primary Key con snake case
            foreach(var key in entity.GetForeignKeys())
            {
                var keyName = key.GetConstraintName();
                if(!string.IsNullOrEmpty(keyName))
                {
                    key.SetConstraintName(ToSnakeCase(keyName));
                }
            }
            //Indexes con snake case
            foreach(var index in entity.GetIndexes())
            {
                var indexName = index.GetDatabaseName();
                if(!string.IsNullOrEmpty(indexName))
                {
                    index.SetDatabaseName(ToSnakeCase(indexName));
                }
            }
            
        }
        // Empezamos a configurar las entidades (crear) 
        // Mapeo entre User y la base de datos
        //Configruracion de la entidad User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(25);
            entity.Property(e => e.Surname)
                .IsRequired()
                .HasMaxLength(25);
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.Email)
                .IsRequired();
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasDefaultValue(false);
            entity.Property(e => e.CreatedAt)
                .IsRequired();
            entity.Property(e => e.UpdatedAt)
                .IsRequired();
            //Indices para la optimizacion de busquedas
            // es decir crea indices unicos en username y email
            entity.HasIndex(e => e.Username).IsUnique();   
            entity.HasIndex(e => e.Email).IsUnique();
            
            //Relaciones
            entity.HasMany(e => e.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);
            entity.HasOne(e => e.UserProfile)
                .WithOne(up => up.User)
                .HasForeignKey<UserProfile>(up => up.UserId);
            entity.HasOne(e => e.UserEmail)
                .WithOne(ue => ue.User)
                .HasForeignKey<UserEmail>(ue => ue.UserId);
            entity.HasOne(e => e.UserPasswordReset)
                .WithOne(up => up.User)
                .HasForeignKey<UserPasswordReset>(up => up.UserId);
        });
        
        //Configuracion de la entidad Role
        // Configuración de UserProfile
        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.UserId)
                .HasMaxLength(16);
            entity.Property(e => e.ProfilePicture).HasDefaultValue("");
            entity.Property(e => e.Phone).HasMaxLength(8);
        });

        // Configuración de Role
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.CreatedAt)
                .IsRequired();
            entity.Property(e => e.UpdatedAt)
                .IsRequired();
            entity.HasMany(e => e.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId);
        });

        // Configuración de UserRole
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.UserId)
                .HasMaxLength(16);
            entity.Property(e => e.RoleId)
                .HasMaxLength(16);
            entity.Property(e => e.CreatedAt)
                .IsRequired();
            entity.Property(e => e.UpdatedAt)
                .IsRequired();
            entity.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            entity.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        });

        // Configuración de UserEmail
        modelBuilder.Entity<UserEmail>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.UserId)
                .HasMaxLength(16);
            entity.Property(e => e.EmailVerified).HasDefaultValue(false);
            entity.Property(e => e.EmailVerificationToken).HasMaxLength(256);
        });
        // Configuración de UserPasswordReset
        modelBuilder.Entity<UserPasswordReset>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.UserId)
                .HasMaxLength(16);
            entity.Property(e => e.PasswordResetToken)
                .HasMaxLength(255);
        });
    }

    // Aca se manejan las entidades que tendran auditoria
    // Estas controlan cuando se crean o modifican los registros
    private void UpdateTimeStamp()
    {
        var entries = ChangeTracker.Entries()
        .Where(e => (e.Entity is User || e.Entity is Role || e.Entity is UserRole)
            && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            if (entry.Entity is User user)
            {
                if(entry.State == EntityState.Added)
                {
                    user.CreatedAt = DateTime.UtcNow;
                }
                user.UpdatedAt = DateTime.UtcNow;
            }
            if (entry.Entity is Role role)
            {
                if(entry.State == EntityState.Added)
                {
                    role.CreatedAt = DateTime.UtcNow;
                }
                role.UpdatedAt = DateTime.UtcNow;
            }
            if (entry.Entity is UserRole userRole)
            {
                if(entry.State == EntityState.Added)
                {
                    userRole.CreatedAt = DateTime.UtcNow;
                }
                userRole.UpdatedAt = DateTime.UtcNow;
            }
        }
    }

    //Este sobreescribe  el metodo SaveChangesAsync para ejecutar la logica de auditoria.
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimeStamp();
        return base.SaveChangesAsync(cancellationToken);
    }
    }

