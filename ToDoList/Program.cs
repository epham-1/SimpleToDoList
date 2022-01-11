using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//Connect to the 'List' Database
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
//Connect to aspIdentityCore
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
//Changing Password Default Settings
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireUppercase = false;
});
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Login";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Add Authentication (Login)
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
