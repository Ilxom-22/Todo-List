﻿using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Todo.Application.Todos;
using Todo.Infrastructure.Todos;
using Todo.Persistence.DataContexts;

using Todo.Persistence.Repositories;

using Todo.Persistence.Repositories.Interfaces;


namespace ToDo.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies;

    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblies(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddBusinessLogicInfrastructure(this WebApplicationBuilder builder)
    {
        // register persistence
        builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TodoList.Database"));

        // register repositories
        builder.Services.AddScoped<ITodoRepository, TodoRepository>();

        // register data access foundation services
        builder.Services.AddScoped<ITodoService, TodoService>();

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    private static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options => { options.AddDefaultPolicy(policyBuilder => { policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }); }
        );

        return builder;
    }

    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}