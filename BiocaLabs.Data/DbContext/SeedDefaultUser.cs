using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BiocaLabs.Data.DbContext;

public class SeedDefaultUser
{
    public static void RegisterDefaultUser(ModelBuilder modelBuilder)
    {
        var user = UserInfo();
        var adminRole = RoleAdmin();
        var userRoles = RoleUser();
        var userRole = UserRole(user.Id, adminRole.Id);
        modelBuilder.Entity<IdentityUser>().HasData(user);
        modelBuilder.Entity<IdentityUser>().HasData(adminRole, userRoles);
        modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey().HasData(userRole);
    }

    // Add User
    private static IdentityUser UserInfo()
    {
        var user = new IdentityUser
        {
            Id = Guid.NewGuid().ToString(),
            Email = "admin@admin.com",
            EmailConfirmed = true,
            UserName = "admin@admin.com"
        };

        var hasher = new PasswordHasher<IdentityUser>();
        user.PasswordHash = hasher.HashPassword(user, "admin_1234");

        return user;
    }

    // Add Admin Role
    private static IdentityRole RoleAdmin()
    {
        var role = new IdentityRole
        {
            Id = Guid.NewGuid().ToString(),
            Name = "admin",
            NormalizedName = "Admin"
        };

        return role;
    }

    // Add User Role
    private static IdentityRole RoleUser()
    {
        var role = new IdentityRole
        {
            Id = Guid.NewGuid().ToString(),
            Name = "user",
            NormalizedName = "User"
        };

        return role;
    }

    // Add Dynamic User Role
    private static IdentityUserRole<string> UserRole(string userId, string roleId)
    {
        var userRole = new IdentityUserRole<string>
        {
            RoleId = roleId,
            UserId = userId
        };
        return userRole;
    }
}