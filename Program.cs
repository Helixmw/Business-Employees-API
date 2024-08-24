using Employees_API.Data;
using Employees_API.DTOs.Departments;
using Employees_API.DTOs.Employees;
using Employees_API.DTOs.Users;
using Employees_API.Interfaces;
using Employees_API.Models;
using Employees_API.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDBContext>()
                    .AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<SignInManager<IdentityUser>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();

//builder.Services.AddScoped<IEmployee, Employee>();
builder.Services.AddScoped<IEmployeesProcessor, EmployeesProcessor>();
builder.Services.AddScoped<IDepartmentsProcessor, DepartmentsProcessor>();
builder.Services.AddScoped<IEmployeeRolesProcessor, EmployeeRolesProcessor>();
builder.Services.AddScoped<IGetEmployeeDTO, GetEmployeeDTO>();
builder.Services.AddScoped<IAddEmployeeDTO, AddEmployeeDTO>();
builder.Services.AddScoped<IAddUserDTO, AddUserDTO>();
builder.Services.AddScoped<ILoginUserDTO, LoginUserDTO>();
builder.Services.AddScoped<IAddDepartmentDTO, AddDepartmentDTO>();
builder.Services.AddScoped<IEditDepartmentDTO, EditDepartmentDTO>();
builder.Services.AddScoped<IUsersProcessor, UserProcessor>();
                

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
