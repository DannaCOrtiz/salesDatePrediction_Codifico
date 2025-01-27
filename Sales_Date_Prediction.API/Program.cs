using Sales_Date_Prediction.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("All", builder =>
    {
        builder.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .SetIsOriginAllowed((Host) => true);
        //.AllowCredentials();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registrar servicios
builder.Services.AddInfrastructure(connectionString);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())); var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("All");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
