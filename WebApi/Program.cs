using Data;
using WebApi.Startup;

var builder = WebApplication.CreateBuilder(args);

WebApiStartup startup = new();
startup.ConfigureServices(builder.Services);
builder.Services.AddSingleton(typeof(IStartupFilter), startup);


builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





var app = builder.Build();


app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
