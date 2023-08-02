using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.S3;
using TattoAPI.Repository.Avigma;
using TattoAPI.Controllers;
using TattoAPI.Data;
using TattoAPI.IRepository;
using TattoAPI.Models;
using TattoAPI.Repository;
using TattoAPI.Repository.Lib;
using CorePush.Apple;
using CorePush.Google;
using Jose;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.OAuth;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITattoService, TattoService>();
//builder.Services.AddTransient<IUserMaster_Data, UserMaster_Data>();
//builder.Services.AddScoped<IUser_Admin_Master_Data, User_Admin_Master_Data>();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<INotification_Data, Notification_Data>();
builder.Services.AddHttpClient<FcmSender>();
builder.Services.AddHttpClient<ApnSender>();
var appSettingsSection = builder.Configuration.GetSection("FcmNotification");
builder.Services.Configure<FcmNotificationSetting>(appSettingsSection);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddDbContext<DbContextClass>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Conn_dBcon")));
//builder.Services.AddDbContext<DbContextClass>();
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
//builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();
builder.Services.AddSingleton<Microsoft.Extensions.Logging.ILogger>(provider =>
   provider.GetRequiredService<Microsoft.Extensions.Logging.ILogger<TattoController>>());

var OAuthOptions = new OAuthAuthorizationServerOptions
{
    AllowInsecureHttp = true,
    TokenEndpointPath = new Microsoft.Owin.PathString("/token"),
    AccessTokenExpireTimeSpan = TimeSpan.FromDays(Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["TokenValidityDays"])),
    Provider = new CustomAuthorizationServerProvider()
};

//builder.Services.Configure<Jwt>(builder.Configuration.GetSection(key: "Jwt"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options=>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew=TimeSpan.Zero
        };
    });
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddMvc();
builder.Services.AddRazorPages();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next();
});

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});
app.Run();
