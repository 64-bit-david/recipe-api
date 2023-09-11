using Microsoft.EntityFrameworkCore;
using RecipeAPI;
using RecipeAPI.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/RecipeApi.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson() //provides for features like patch updates
    .AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<RecipeDataStore>();

builder.Services.AddDbContext<RecipeContext>(dbContextOptions =>
    dbContextOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServerConnection"]));

builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.MapControllers();

app.Run();
