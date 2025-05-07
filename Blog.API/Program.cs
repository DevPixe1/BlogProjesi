using Blog.API.Extensions;
using Blog.Service.Mapping;
using Blog.Service.Validators;
using FluentValidation.AspNetCore;
using FluentValidation;
using Blog.Service.Validations;

var builder = WebApplication.CreateBuilder(args);

// Proje servislerini merkezi olarak ekler (DbContext, AutoMapper, FluentValidation, Repos, Services, UnitOfWork vs.)
builder.Services.AddProjectServices(builder.Configuration);

// FluentValidation konfig�rasyonu
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePostDtoValidator>(); // BURASI �NEML�

// AutoMapper konfig�rasyonu
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Controller ve Swagger servisleri
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Geli�tirme ortam�nda Swagger'� aktif eder
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
