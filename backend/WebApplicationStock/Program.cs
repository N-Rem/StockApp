

using Application.Services;
using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//
#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalHost",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173", "http://localhost:5174", "http://localhost:5175")
            .AllowAnyHeader()
            .AllowAnyMethod();

        });

});
#endregion

builder.Services.AddSwaggerGen();




#region MySql

string connectionString = builder.Configuration.GetConnectionString("DBConnectionString")!;

builder.Services.AddDbContext<AppDbContext>(opcion =>
{
    opcion.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
        b => b.MigrationsAssembly("Infrastructure"));
    opcion.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.AmbientTransactionWarning));
});

#endregion

#region Repositories
//se inyecta RepositoryBase para poder hacer un Service Base.
builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();
builder.Services.AddScoped<IRepositoryProduct, RepositoryProduct>();
builder.Services.AddScoped<IRepositoryBranch, RepositoryBranch>();
builder.Services.AddScoped<IRepositoryHistoricalPrice, RepositoryHistoricalPrice>();
#endregion

#region Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IHistoricalPriceService, HistoricalPriceService>();  

#endregion

var app = builder.Build();

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
