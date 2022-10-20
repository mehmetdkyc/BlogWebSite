using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));

});
builder.Services.AddMvc();
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
    {
        x.LoginPath = "/Login/Index";
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(500);

});

builder.Services.AddDbContext<Context>(options => options.UseSqlServer("Server=DESKTOP-QFIKDEC; Database=DotnetCoreKampý; TrustServerCertificate=True; Trusted_Connection=True;"));
builder.Services.AddTransient<IBlogDal, EfBlogRepository>();
builder.Services.AddTransient<BlogManager>();

builder.Services.AddTransient<IAboutDal, EfAboutRepository>();
builder.Services.AddTransient<AboutManager>();

builder.Services.AddTransient<IWriterDal, EfWriterRepository>();
builder.Services.AddTransient<WriterManager>();

builder.Services.AddTransient<ICategoryDal, EfCategoryRepository>();
builder.Services.AddTransient<CategoryManager>();

builder.Services.AddTransient<ICommentDal, EfCommentRepository>();
builder.Services.AddTransient<CommentManager>();

builder.Services.AddTransient<IContactDal, EfContactRepository>();
builder.Services.AddTransient<ContactManager>();

builder.Services.AddTransient<INotificationDal, EfNotificationRepository>();
builder.Services.AddTransient<NotificationManager>();

builder.Services.AddTransient<INewsLetterDal, EfNewsLetterRepository>();
builder.Services.AddTransient<NewsLetterManager>();

builder.Services.AddTransient<IMessageDal, EfMessageRepository>();
builder.Services.AddTransient<MessageManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error/ErrorPage/", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}");

app.Run();
