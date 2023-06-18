using Beer_API.Services;
using Beers.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


// Add services to the container.
builder.Services.AddHttpClient<IPunkApiService, PunkApiService>(client =>
{
    client.BaseAddress = new Uri("https://api.punkapi.com/v2/");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Enable CORS
app.UseCors();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Run the background service
//var beerEmailService = app.Services.GetService<BeerRandomEmailService>();
//var cancellationToken = app.Services.GetService<IHostApplicationLifetime>().ApplicationStopping;
//Task.Run(() => beerEmailService.ExecuteAsync(cancellationToken));


app.Run();
