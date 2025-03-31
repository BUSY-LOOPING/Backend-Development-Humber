using Cumulative1.Controllers;
using Cumulative1.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

// Database
builder.Services.AddScoped<SchoolDbContext>();
builder.Services.AddScoped<TeacherAPIController>(); //important !!!!
builder.Services.AddScoped<StudentAPIController>(); //important !!!!
builder.Services.AddScoped<CourseAPIController>(); //important !!!!


// Add services to the container.
builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
