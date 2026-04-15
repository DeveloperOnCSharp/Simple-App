using contact_app.extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServicesCollection(builder.Configuration);

var app = builder.Build();
app.Services.AddCustomServices(builder.Configuration);
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("CorsPolicy");
app.MapControllers();
app.Run();
