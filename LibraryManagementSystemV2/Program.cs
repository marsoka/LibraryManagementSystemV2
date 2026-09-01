using FluentValidation;
using Library.API.Extensions;
using Library.API.Middleware;
using Library.Application;
using Library.Application.Abstractions.Repositories;
using Library.Application.Interfaces;
using Library.Application.Mapping;
using Library.Application.Services;
using Library.Domain;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"));
}
);

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IBookService, BookServices>();
builder.Services.AddScoped<IMemberService, MemberServices>();
builder.Services.AddScoped<IBorrowingServices, BorrowingServices>();
// builder.Services.AddScoped(IBaseRepository, BaseRepository);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();




builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));




builder.Services.AddAutoMapper(cfg =>
    {
        cfg.AddMaps(typeof(AuthorProfile).Assembly);
    });


builder.Services.AddValidatorsFromAssemblyContaining<CreateAuthorValidator>();

builder.Services.AddJwtAuthentication(builder.Configuration);


builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
