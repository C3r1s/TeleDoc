using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TeleDoc.API.Filters;
using TeleDoc.Application.DTOs.Client;
using TeleDoc.Application.Interfaces.Services;
using TeleDoc.Application.Mappings;
using TeleDoc.Application.Services;
using TeleDoc.Application.Validators.Client;
using TeleDoc.Data;
using TeleDoc.Infrastructure.Interfaces;
using TeleDoc.Infrastructure.Repositories;

namespace TeleDoc.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        
    }

    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddApplication();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IFounderService, FounderService>();
    }

    public static void ConfigureApiServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<ClientProfile>();
            cfg.AddProfile<FounderProfile>();
        });
        services.AddHttpContextAccessor();
    }

    public static void AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IFounderRepository, FounderRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<LogActionFilter>();

    }

    private static void AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssemblyContaining<IndividualEntrepreneurCreateDtoValidator>();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IValidator<LegalEntityCreateDto>, LegalEntityCreateDtoValidator>();
    }
}