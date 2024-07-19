using BiocaLabs.Data.DbContext;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace BiocaLabs.IntegrationTests.IntegrationTests;

public abstract class IntegrationTestBase : IAsyncLifetime
{
    private PostgreSqlContainer? _container;
    protected WebApplicationFactory<Program>? Factory { get; private set; }

    public async Task InitializeAsync()
    {
        _container = new PostgreSqlBuilder()
            .WithDatabase("testdb")
            .WithUsername("postgres")
            .WithPassword("testpassword")
            .WithPortBinding(5433)
            .Build();

        await _container.StartAsync();

        var host = _container.Hostname;
        var port = _container.GetMappedPublicPort(5433);

        var connectionString =
            $"Host={host};Port={port};Database=testdb;Username=postgres;Password=testpassword";

        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(connectionString));
                });
            });

        using var scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        if (_container is not null)
        {
            await _container.StopAsync();
            await _container.DisposeAsync();
        }

        if (Factory is not null) await Factory.DisposeAsync();
    }
}