using hexagonal.Application;
using hexagonal.Application.Components.AuthenticationComponent.Commands;
using hexagonal.Application.Components.AuthenticationComponent.SecurityCore;
using hexagonal.Application.Components.AuthenticationComponent.SecurityCore.UseCases;
using hexagonal.Application.Components.AuthenticationComponent.SecurityCore.Validation;
using hexagonal.Application.Components.BookComponent.Commands;
using hexagonal.Application.Components.BookComponent.Core;
using hexagonal.Application.Components.BookComponent.Core.UseCases;
using hexagonal.Application.Components.BookComponent.Core.Validations;
using hexagonal.Application.Components.BookComponent.Queries;
using hexagonal.Application.Components.CategoryComponent.Queries;

namespace hexagonal.API.Modules;

/// <summary>
///     Adds Use Cases classes.
/// </summary>
public static class UseCasesExtensions
{
    /// <summary>
    ///     Adds Use Cases to the ServiceCollection.
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();

        #region Authentication

        // Application - Services
        services.AddScoped<IAuthenticationCommand, AuthenticationCommand>();

        // Core - UseCases
        services.AddScoped<IUcValidateLogin, UcValidateLogin>();
        services.AddScoped<IUcGenerateToken, UcGenerateToken>();

        services.AddScoped<ISystemUserPasswordValidation, SystemUserPasswordValidation>();

        #endregion

        #region Book

        // Application - Services
        services.AddScoped<IBookCommand, BookCommand>();
        services.AddScoped<IBookQuery, BookQuery>();


        // Core - UseCases
        services.AddScoped<IUcBookEdit, UcBookEdit>();
        services.AddScoped<IUcBookCreate, UcBookCreate>();
        services.AddScoped<IUcBookDelete, UcBookDelete>();


        // Core - Validations
        services.AddScoped<IBookEditValidation, BookEditValidation>();
        services.AddScoped<IBookDeleteValidation, BookDeleteValidation>();
        services.AddScoped<IBookCreateValidation, BookCreateValidation>();

        #endregion


        #region Category

        // Application - Services
        services.AddScoped<ICategoryQuery, CategoryQuery>();

        #endregion

        return services;
    }
}