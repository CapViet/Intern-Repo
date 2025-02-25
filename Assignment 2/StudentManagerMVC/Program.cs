using StudentManagerMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using StudentManagerMVC.Areas.Identity.Data;
using StudentManagerMVC.Services.ScoreServ;
using StudentManagerMVC.Repositories.StudentRepo;
using StudentManagerMVC.Repositories.ScoresRepo;
using StudentManagerMVC.Services.StudentServ;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDbContext<AuthenticationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDefaultIdentity<StudentManagerMVCUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AuthenticationContext>();

// Register the Student services and repositories
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

// Register the Scores services and repositories
builder.Services.AddScoped<IScoresRepository, ScoresRepository>();
builder.Services.AddScoped<IScoresService, ScoresService>();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
