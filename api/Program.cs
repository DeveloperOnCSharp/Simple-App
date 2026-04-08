var builder = WebApplication.CreateBuilder(args);
// Создаем переменную для хранения списка разрешенных доменов
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Contact API",
    });
});
builder.Services.AddControllers();
builder.Services.AddSingleton<ContactStorage>();

builder.Services.AddCors(opt =>
//Добавляем политику CORS с именем "CorsPolicy" и задаем правила
opt.AddPolicy("CorsPolicy", policy =>
{
    policy.AllowAnyMethod()
    .AllowAnyHeader()
    .WithOrigins(allowedOrigins);

}));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("CorsPolicy");
app.MapControllers();
app.Run();

