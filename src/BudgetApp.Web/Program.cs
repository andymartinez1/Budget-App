using BudgetApp.Data;
using BudgetApp.Entities;
using BudgetApp.ServiceContracts;
using BudgetApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    if (builder.Configuration["DisableAntiforgery"] == "true")
        options.Filters.Add(new IgnoreAntiforgeryTokenAttribute());
});
builder.Services.AddHttpLogging();

if (builder.Configuration["UseInMemoryDatabase"] == "true")
    builder.Services.AddDbContext<BudgetDbContext>(options =>
        options.UseInMemoryDatabase("BudgetAppDb")
    );
else
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

builder.Services.AddScoped<ITransactionService, TransactionService>();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}").WithStaticAssets();

app.Run();

public partial class Program { }
