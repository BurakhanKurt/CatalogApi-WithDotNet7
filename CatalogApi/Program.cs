using Catalog.Entity.Extesions;
using Catalog.Entity.Logging.Abstract;
using Microsoft.AspNetCore.Mvc;
using NLog;


var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(),
    "/nlog.config"));

// Add services to the container.


builder.Services.AddControllers(config =>
{
    config.CacheProfiles.Add("30second", new CacheProfile() { Duration = 30 });
}) 
//Json loop ignore
.AddNewtonsoftJson(opt =>
 {
     opt.SerializerSettings.ReferenceLoopHandling =
     Newtonsoft.Json.ReferenceLoopHandling.Ignore;
 });



//ActionFilter
builder.Services.ConfigureActionFilter();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//----EXTENCIONS----//

//Sql Context
builder.Services.ConfigureSqlContext(builder.Configuration);

//Repository
builder.Services.RegisterRepositories();

//Entity
builder.Services.ConfigureServices();

//Cache
builder.Services.ConfigureMemoryCaching();

//auto map
builder.Services.ConfigureMapProfile();

//ResponseCaching
builder.Services.ConfigureResponseCaching();


//------------------//


var app = builder.Build();

//mw
var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();
