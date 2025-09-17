using AutoMapper;
using Blog.Application.Common.Mapping;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Services;
using Blog.Domain.Entities;
using Blog.Domain.Identity;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//app services
builder.Services
    .AddScoped<IArticleService, ArticleService>()
    .AddScoped<ICommentService, CommentService>()
    .AddScoped<ITagService, TagService>()
    .AddScoped<IUserService, UserService>();

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

builder.Services.AddControllersWithViews();
//builder.Services.AddControllers();
builder.Services.AddRazorPages();
//builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogApi", Version = "v1" }); });
//builder.Services.AddCors(opt =>
//{
//    opt.AddPolicy("AllowAll", builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});

//---builder tio app---
var app = builder.Build();


//middlewares
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseDeveloperExceptionPage();
//app.UseSwagger();
//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogApi v1"));

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
//app.UseCors("AllowAll");

//endpoints
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();
