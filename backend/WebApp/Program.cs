using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using App.BLL;
using App.Contracts.BLL;
using App.Contracts.DAL;
using App.Domain;
using App.DAL.EF;
using App.Domain.Identity;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp;
using WebApp.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddScoped<IAppUnitOfWork, AppUOW>();
builder.Services.AddScoped<IAppBLL, AppBLL>();

builder.Services
    .AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultUI()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// clear default claims
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services
    .AddAuthentication(o =>
    {
        o.DefaultScheme = "MyCookieScheme";
        o.DefaultAuthenticateScheme = "MyCookieScheme";
    })
    .AddCookie("MyCookieScheme",  o =>
    {
        o.Cookie.Name = "jwt";
        o.Cookie.HttpOnly = true;
        o.Cookie.SecurePolicy = CookieSecurePolicy.None;
        o.Cookie.SameSite = SameSiteMode.Strict;
        //o.Cookie.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        o.Cookie.IsEssential = true;
        o.Cookie.Path = "/";
    });

builder.Services.AddAuthorization(
    b =>
    {
        b.AddPolicy("id_policy", pb => pb
            .RequireAuthenticatedUser()
            .AddAuthenticationSchemes("MyCookieScheme")
            .RequireClaim(ClaimTypes.NameIdentifier)
        );
    }
    );

builder.Services.AddAutoMapper(
    typeof(App.DAL.EF.AutoMapperProfile),
    typeof(App.BLL.AutoMapperProfile),
    typeof(WebApp.Helpers.AutoMapperProfile)
);

var apiVersioningBuilder = builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});
apiVersioningBuilder.AddApiExplorer(options =>
{
    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
    // note: the specified format code will format the version as "'v'major[.minor][-status]"
    options.GroupNameFormat = "'v'VVV";

    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    // can also be used to control the format of the API version in route templates
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddCors(options =>
{
   options.AddPolicy("AllowSpecificOrigin",
       policy => { policy.WithOrigins(builder.Configuration.GetValue<string>("AllowedFrontendDomain"))
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials(); // Allow credentials (cookies)
            });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();

// ===================================================
var app = builder.Build();
// ===================================================

SetupAppData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// app.UseCors("CorsAllowAll");
app.UseCors("AllowSpecificOrigin");

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();



app.UseRequestLocalization(options:
    app.Services.GetService<IOptions<RequestLocalizationOptions>>()?.Value!
);

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant()
        );
    }
    // serve from root
    // options.RoutePrefix = string.Empty;
});

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); //.RequireAuthorization("cookie");

app.Run();

static void SetupAppData(WebApplication app)
{
    using var serviceScope = ((IApplicationBuilder)app).ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope();
    using var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    if (!context.Database.ProviderName!.Contains("InMemory"))
    {
        context.Database.Migrate();
    }
    
    using var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    using var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
    
    var res = roleManager.CreateAsync(new AppRole()
    {
        Name = "Admin"
    }).Result;

    if (!res.Succeeded)
    {
        Console.WriteLine(res.ToString());
    }

    var user = new AppUser()
    {
        Email = "admin@eesti.ee",
        UserName = "admin@eesti.ee",
        FirstName = "Admin",
        LastName = "Eesti"
    };
    res = userManager.CreateAsync(user, "Kala.maja1").Result;
    if (!res.Succeeded)
    {
        Console.WriteLine(res.ToString());
    }

    res = userManager.AddToRoleAsync(user, "Admin").Result;
    if (!res.Succeeded)
    {
        Console.WriteLine(res.ToString());
    }

    if (!context.PaymentMethods.Any())
    {
        DateTime dt = DateTime.Now.ToUniversalTime();
        context.PaymentMethods.Add(new PaymentMethod()
        {
            MethodName = "sularaha",
            MethodDescription = "Makse sularahas",
            Active = true,
            UpdatedAt = dt,
            UpdatedBy = "Intial data creation",
            CreatedBy = "Initial data creation",
            CreatedAt = dt
        });
        context.PaymentMethods.Add(new PaymentMethod()
        {
            MethodName = "pangaülekanne",
            MethodDescription = "Makse pangaülekandega",
            Active = true,
            UpdatedAt = dt,
            UpdatedBy = "Intial data creation",
            CreatedBy = "Initial data creation",
            CreatedAt = dt
        });
        
        context.SaveChanges();
    }
}