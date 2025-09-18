using AutoMapper;
using Blog.Application.Common.Mapping;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Services;
using Blog.Domain.Entities;
using Blog.Domain.Identity;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Repositories;
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
    .AddDbContext<BlogDbContext>(opt => opt.UseSqlite(connection))
    //.AddDbContext<BlogDbContext>(opt => opt.UseSqlServer(connection)) sqlserver
    .AddUnitOfWork()
    .AddCustomRepository<Article, ArticlesRepository>()
    .AddCustomRepository<Comment, CommentsRepository>()
    .AddCustomRepository<Tag, TagsRepository>()
    .AddIdentity<User, Role>(opt =>
        {
            opt.Password.RequiredLength = 5;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireDigit = false;
        })
    .AddEntityFrameworkStores<BlogDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


//---build to app---
var app = builder.Build();

//roles
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider.GetService<IUserService>();
    if (service != null)
    {
        await service.CreateRoleAsync(SystemRoles.User);
        await service.CreateRoleAsync(SystemRoles.Admin);
        await service.CreateRoleAsync(SystemRoles.Moderator);
    }
}
//middlewares
if (!app.Environment.IsDevelopment())
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
