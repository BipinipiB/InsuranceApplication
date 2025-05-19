using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Insurance.DataAccess.Data;
using Insurance.Models;
using Insurance.DataAccess.Services;
using Insurance.DataAccess.Repository;
using Insurance.DataAccess.Repository.IRepository;
using Insurance.Services;
using Insurance.Services.Security;

//This is application Entry point
//initializes the application with the required settings
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

//Add Identity service to application
builder.Services.AddIdentity<ApplicationUser, IdentityRole>() //ApplicationUser is User Entity and Identity Role is role entity
    .AddEntityFrameworkStores<ApplicationDbContext>() //specifies that entity framework will be used to store the identity information
    .AddDefaultTokenProviders(); //adds default token provider that can be used to generate tokens for things like password reset,email confirmation etc

// Add services to the container.
builder.Services.AddControllersWithViews();

//register PolicyTypes Service
builder.Services.AddScoped<IPolicyTypeService, PolicyTypeService>();

//register UserRepository Service
builder.Services.AddScoped<IUserRepository, UserRepository>();

//register PolicyRepository Service
builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();

//register UnitOfWork Service
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//register UserService Service
builder.Services.AddScoped<IUserService, UserService>();

//register PolicyService Service
builder.Services.AddScoped<IPolicyService, PolicyService>();

//register QuestionService
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

//register ClaimService
builder.Services.AddScoped<IClaimService, ClaimsService>();

//register ClaimRepository
builder.Services.AddScoped<IClaimRepository, ClaimRepository>();

//Register SessionDataProtector
builder.Services.AddScoped<SessionDataProtector>();

// Add session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


////allows access to the current HTTP context, which is useful for accessing session data and user information
////used to get current user information in the application in service classes
//builder.Services.AddHttpContextAccessor(); 

//Create and Configure the web application
var app = builder.Build();

//call SeedData to create roles

//creates a new scope for dependency injection
//scope ensures that services with a scoped lifetime are correctly managed and disposed of when no longer needed
using (var scope = app.Services.CreateScope())
{
    //retrives the IServiceProvider for created scope
    //IserviceProvider is responsible for providing instances of the services registerd in dependency injection container
    var services = scope.ServiceProvider;
    SeedData.Initialize(services).Wait();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//These are middleware configuration for routing,authentication
//and authorization
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//Register the session middleware
app.UseSession();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
