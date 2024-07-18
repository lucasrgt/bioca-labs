﻿using Lab.Application.UseCases;
using Lab.Domain.Repositories;
using Lab.Infrastructure.DbContext;
using Lab.Infrastructure.RepositoriesImpl;
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

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Lab
        services.AddScoped<IMedicineRepository, MedicineRepositoryImpl>();

        return services;
    }

    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        // Lab
        services.AddScoped<CreateMedicine>();

        return services;
    }
}