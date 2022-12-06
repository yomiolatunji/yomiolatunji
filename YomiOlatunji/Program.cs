using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using YomiOlatunji.Core.DbModel;
using YomiOlatunji.DataSource;
using YomiOlatunji.DataSource.Interface;
using YomiOlatunji.DataSource.Repository;
using YomiOlatunji.DataSource.Services;
using YomiOlatunji.DataSource.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddRazorPages();

builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<ILogRepository, LogRepository>();
builder.Services.AddTransient<IPostTagRepository, PostTagRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IContactMessageRepository, ContactMessageRepository>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<IContactMessageService, ContactMessageService>();
builder.Services.AddTransient<IFileManager, FileManager>();
builder.Services.AddTransient<IPostManager, PostManager>();
builder.Services.AddTransient<IUploadImageService, UploadImageService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))); 

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.MapGet("/api/test", (IUploadImageService uploadService) =>
{
    return Results.Ok(uploadService.Test());
});
app.MapPost("/api/save-image", (HttpContext httpContext, IUploadImageService uploadService) =>
{
    IFormFile file1 = null;
    foreach (var file in httpContext.Request.Form.Files)
    {
        if (file.Length > 0)
        {
            file1= file;
            break;
        }
    }
    return Results.Ok(uploadService.Upload(file1));
});
app.MapGet("/api/image", (string filename, IUploadImageService uploadService) =>
{
    return Results.Ok(filename);
});

SeedUserDataService.Initialize(app.Services);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

