using WeatherApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IDateParser, DateParser>();
builder.Services.AddHttpClient<IWeatherClient, OpenMeteoWeatherClient>();
builder.Services.AddSingleton<IWeatherStorage, FileWeatherStorage>();
builder.Services.AddScoped<IWeatherService, WeatherService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseCors();
app.UseRouting();
app.MapControllers();

app.Run();
