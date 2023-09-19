using CustomerAPI.CasosDeUsos;
using CustomerAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(routing => routing.LowercaseUrls = true);

builder.Services.AddDbContext<CustomerDatabaseContext>(mysqlbuilder =>
{
    mysqlbuilder.UseMySQL(builder.Configuration.GetConnectionString("cnn"));
});

builder.Services.AddScoped<UpdateCustomerUserCas, UpdateCustomerUserCase>();


//builder.Services.AddCors(option =>
//{
//    option.AddDefaultPolicy(builder =>
//    {
//        builder.AllowAnyHeader();
//        builder.AllowAnyMethod();
//        builder.AllowAnyOrigin();
//    });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
