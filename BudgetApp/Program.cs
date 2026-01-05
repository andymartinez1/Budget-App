using BudgetApp.Data;
using BudgetApp.Models;
using BudgetApp.Repository;
using BudgetApp.Repository.Interfaces;
using BudgetApp.Services;
using BudgetApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpLogging();
builder.Services.AddDbContext<BudgetDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BudgetDbContext"))
);

builder
    .Services.AddIdentity<BudgetUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 8;
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    })
    .AddEntityFrameworkStores<BudgetDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Initialize the database with seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedDatabase.InitializeTransactions(services);
    app.Logger.LogInformation(1, "Database seeded and ready.");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseHttpLogging();

app.MapStaticAssets();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}").WithStaticAssets();

app.Run();
