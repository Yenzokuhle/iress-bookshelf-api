using Iress_API_bookshelf.EF.Context;
using Iress_API_bookshelf.Services;
using Iress_API_bookshelf.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



/**
 * Middleware 1
 */
builder.Services.AddCors(options => options.AddPolicy("developmentCORS", policy =>
{
    policy
    .AllowAnyHeader()
    .WithOrigins("*")
    .AllowAnyMethod()
    .Build();
}));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/**
 * Middleware AddDbContext 2
 */
var connectionString = builder.Configuration.GetConnectionString("BooksDBConnection");
builder.Services.AddDbContext<BooksContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IBookRepositoryService, BookRepositoryService>();
builder.Services.AddScoped<IBusinessLogicService, BusinessLogicService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("developmentCORS");
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "images")),
    RequestPath = "/images"
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
