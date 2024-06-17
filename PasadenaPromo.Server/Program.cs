using System.Text;
using System.Text.RegularExpressions;
using Markdig;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PasadenaPromo;
using PasadenaPromo.Auth;

var builder = WebApplication.CreateBuilder(args);

var markdown = File.ReadAllText("./README.md");
var html = Markdown.ToHtml(markdown);
var linkStyles = """<link href="style.css" rel="stylesheet" />""";
html += linkStyles;
var hostName = builder.Configuration["HostName"];
html = Regex.Replace(html, "{{.+}}", hostName);
var index = "./wwwroot/index.html";
var file = new FileStream(index, FileMode.Create);
file.Close();
File.WriteAllText(index, html, Encoding.Unicode);

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddJsonFile("secrets.json");
}

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Add 'DefaultConnection' to appsettings");
var dbUsername = builder.Configuration["db-user"] ?? throw new Exception("Add 'db-user' to secrets");
var dbPassword = builder.Configuration["db-password"] ?? throw new Exception("Add 'db-password' to secrets");
connectionString += $" User Id={dbUsername}; Pwd={dbPassword};";
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

var jwtSettings = new JwtSettings();
builder.Configuration.Bind("JwtSettings", jwtSettings);
jwtSettings.Key = builder.Configuration["jwt-key"] ?? throw new Exception("Add 'jwt-key' to secrets");

builder.Services.AddSingleton(jwtSettings);
builder.Services.AddTransient<JwtTokenCreator>();
builder.Services.AddTransient<HashService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddAuthentication(i =>
{
    i.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    i.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    i.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
        ClockSkew = jwtSettings.Expire
    };
    options.SaveToken = true;
    options.Events = new JwtBearerEvents();
    options.Events.OnMessageReceived = context =>
    {
        if (context.Request.Cookies.ContainsKey("X-Access-Token"))
        {
            context.Token = context.Request.Cookies["X-Access-Token"];
        }
        return Task.CompletedTask;
    };
})
.AddCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseFileServer();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();