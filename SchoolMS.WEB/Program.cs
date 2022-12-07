using SchoolLMS.Bll.Email;
using SchoolLMS.Bll.Role;
using SchoolLMS.Bll.Student;
using SchoolLMS.Bll.Users;
using SchoolLMS.Dal.Dapper;
using SchoolLMS.Dal.Repos;
using SchoolLMS.WEB.Infrasruture;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddScoped<IUserRoleRepository, UserRoleRopository>();
builder.Services.AddScoped<IStudentService, StudentServices>();
builder.Services.AddScoped<IRoleServices, RoleServices>(); 
builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddScoped<IEmailServices, EmailServices>();
builder.Services.AddScoped<IDapperRepository, DapperRepository>();





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
