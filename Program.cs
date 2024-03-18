using System.Text.Json.Serialization;
using test_library.Respository;
using test_library.Utils;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(j =>
{
    j.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    j.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

});
builder.Services.AddScoped<IBookRespository, BookRespository>();
builder.Services.AddSingleton<DbContextData>();

var app = builder.Build();
{
    using var appscope = app.Services.CreateScope();
    var context = appscope.ServiceProvider.GetRequiredService<DbContextData>();
    await context.Init();
}
app.UseHttpsRedirection();
app.UseMiddleware<MiddlewareApiErrorHandler>();
app.MapControllers();

app.MapGet("/", () => "Библиотека запущена");

app.Run();
