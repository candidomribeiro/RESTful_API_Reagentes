global using Microsoft.AspNetCore.Authentication.Cookies;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Reagentes.Models;
global using System;
global using System.Collections.Generic;
global using System.ComponentModel.DataAnnotations;
global using System.Linq;
global using System.Threading.Tasks;
global using Swashbuckle.AspNetCore.Annotations;

using Microsoft.OpenApi.Models;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;


// **************************  ConfigureServices  **************************

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseMySQL(Configuration["ConnectionStrings:DefaultConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

builder.Services.AddDbContext<IdentityContext>(o =>
{
    o.UseMySQL(Configuration["ConnectionStrings:IdentityConnection"]);
    o.EnableSensitiveDataLogging(true);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();

builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.Password.RequiredLength = 6;
    opts.Password.RequiredUniqueChars = 1;
    opts.Password.RequireDigit = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireNonAlphanumeric = false;
    opts.User.RequireUniqueEmail = false;
    // opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";
});

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(o =>
{
    o.Events.DisableRedirectForPath(e => e.OnRedirectToLogin, "/api", StatusCodes.Status401Unauthorized);
    o.Events.DisableRedirectForPath(e => e.OnRedirectToAccessDenied, "/api", StatusCodes.Status403Forbidden);
    o.ExpireTimeSpan = TimeSpan.FromHours(12);
    o.SlidingExpiration = false;
});

string[] origins = Configuration.GetSection("Cors:AllowedOrigins").Get<List<string>>().ToArray();
string[] headers = Configuration.GetSection("Cors:AllowedHeaders").Get<List<string>>().ToArray();
string[] methods = Configuration.GetSection("Cors:AllowedMethods").Get<List<string>>().ToArray();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(origins)
               .AllowCredentials()
               .WithHeaders(headers)
               .WithMethods(methods);
    });
});

builder.Services.AddControllers();

// add 'Swagger' to auto document the RESTful_Reagentes API
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v2.1.0", new OpenApiInfo { Title = "RESTful_API_Reagentes", Version = "v2.1.0" });
});


// **************************  Configure  **************************

WebApplication app = builder.Build();

IWebHostEnvironment env = builder.Environment;

if (env.IsDevelopment())
{ 
    app.UseDeveloperExceptionPage(); 
}
else if (env.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// app.UseResponseCompression();

// configure 'Swagger' endpoint
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v2.1.0/swagger.json", "RESTful_API_Reagentes");
});


// Seed Users and Roles data in ASP.NET Core Identity
string[] ��ɫĿ¼ = Configuration.GetSection("��ɫĿ¼:��ɫ").Get<List<string>>().ToArray();
string �û��� = Configuration.GetSection("��ʦ:�û���").Value;
string ���� = Configuration.GetSection("��ʦ:����").Value;
string ���� = Configuration.GetSection("��ʦ:����").Value;
string �ʼ� = Configuration.GetSection("��ʦ:�ʼ�").Value;
string �绰���� = Configuration.GetSection("��ʦ:�绰����").Value;
string ��ɫ = Configuration.GetSection("��ʦ:��ɫ").Value;

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var dataContext = serviceProvider.GetRequiredService<DataContext>();

        Reagentes.IdentityDataInitializer.SeedRoles(roleManager, ��ɫĿ¼);
        Reagentes.IdentityDataInitializer.SeedUsers(userManager, dataContext, �û���, ����, �绰����, ����, ��ɫ, �ʼ�);
    }
    catch (Exception e)
    {
        System.Diagnostics.Debug.WriteLine(e);
    }
}

// **************************  Application Run  **************************
app.Run();
