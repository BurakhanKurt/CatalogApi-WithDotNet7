using Catalog.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
//Json loop ignore
.AddNewtonsoftJson(opt =>
 {
     opt.SerializerSettings.ReferenceLoopHandling =
     Newtonsoft.Json.ReferenceLoopHandling.Ignore;
 });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//----EXTENCIONS----//
//Sql Context
builder.Services.ConfigureSqlContext(builder.Configuration);
//Repository
builder.Services.RegisterRepositories();
//Service
builder.Services.ConfigureServices();
//Cache
builder.Services.ConfigureMemoryCaching();
//------------------//

var app = builder.Build();
app.ConfigureExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Caching
app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();
