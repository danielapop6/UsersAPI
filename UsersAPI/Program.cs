using Microsoft.EntityFrameworkCore;
using Users.Business;
using Users.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<RepositoryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankDatabase"));
});

builder.Services.ConfigureRepositories();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
