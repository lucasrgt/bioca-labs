using BiocaLabs.Data.DbContext;
using Lab.Application.UseCases;
using Lab.Domain.Repositories;
using Lab.Infrastructure.RepositoriesImpl;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BiocaLabs.API.IoC;

public static class ServiceExtensions
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Database Connection String was not found.", nameof(connectionString));

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }

    public static IServiceCollection SetupIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        }).AddEntityFrameworkStores<AppDbContext>().AddRoles<IdentityRole>().AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Lab
        services.AddScoped<IMedicineRepository, MedicineRepositoryImpl>();

        return services;
    }

    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        // Lab
        services.AddScoped<GetMedicineByName>();
        services.AddScoped<CreateMedicine>();
        services.AddScoped<UpdateMedicine>();
        services.AddScoped<DeleteMedicine>();

        return services;
    }
}