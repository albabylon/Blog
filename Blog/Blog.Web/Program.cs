using AutoMapper;
using Blog.Application.Common.Mapping;
using Blog.Domain.Entities;
using Blog.Domain.Identity;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//mapping
var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//db connection
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services
    .AddDbContext<BlogDbContext>(opt => opt.UseSqlServer(connection))
    .AddUnitOfWork()
    .AddCustomRepository<Article, ArticlesRepository>()
    .AddCustomRepository<Comment, CommentsRepository>()
    .AddCustomRepository<Tag, TagsRepository>()
    .AddIdentity<User, IdentityRole>(opt =>
        {
            opt.Password.RequiredLength = 5;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireDigit = false;
        })
    .AddEntityFrameworkStores<BlogDbContext>();

//builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogApi", Version = "v1" }); });


//---builder tio app---
var app = builder.Build();


//middlewares
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogApi v1"));
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

//endpoints
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();
