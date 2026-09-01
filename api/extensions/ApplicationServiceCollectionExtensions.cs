using Microsoft.OpenApi;
using contact_app.storage;
using Microsoft.EntityFrameworkCore;
using contact_app.dataContext;
using contact_app.seed;

namespace contact_app.extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddServicesCollection(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        // Создаем переменную для хранения списка разрешенных доменов
        // Создаем переменную для хранения подключения к базе данных
        var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();
        var connectionString = configuration.GetConnectionString("SQLiteConnectionString");
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Contact API",
            });
        });
        services.AddControllers();
        services.AddDbContext<SqliteDbContext>(options =>
            options.UseSqlite(connectionString));
        services.AddScoped<IPaginationStorage, PaginationSqliteEFStorage>();
        services.AddScoped<IInitializer, SqliteEfFakerInitializer>();
        // services.AddSingleton<IStorage>(new SqliteStorage(connectionString));
        services.AddCors(opt =>
        //Добавляем политику CORS с именем "CorsPolicy" и задаем правила
        opt.AddPolicy("CorsPolicy", policy =>
        {
            policy.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(allowedOrigins);

        }));

        return services;
    }
}
