using AutoMapper;
using Blog.Application.Common.Mapping;
using Blog.Domain.Entities;
using Blog.Domain.Identity;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Debug);
});
var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()), loggerFactory);

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services
    .AddDbContext<BlogDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")))
    .AddUnitOfWork()
    .AddCustomRepository<Tag, TagsRepository>();

builder.Services
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
builder.Services.AddRazorPages();

//swagger


//---builder tio app---
var app = builder.Build();


// Configure the HTTP request pipeline.
//middlewares
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

//endpoints
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
