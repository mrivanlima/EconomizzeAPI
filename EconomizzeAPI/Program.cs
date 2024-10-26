using Economizze.Library;
using EconomizzeAPI.Services.Cache;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories;
using EconomizzeAPI.Services.Repositories.Classes;
using EconomizzeAPI.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;


builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(options =>
{
	options.ReturnHttpNotAcceptable = true;
}).AddXmlSerializerFormatters();

builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>
{
	x.TokenValidationParameters = new TokenValidationParameters
	{
		ValidIssuer = config["JwtSettings:Issuer"],
		ValidAudience = config["JwtSettings:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey
					(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true

    };

});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


builder.Services.AddAuthorization();
builder.Services.AddScoped<IConnectionService, ConnectionService>();




//User
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserLoginRepository, UserLoginRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserAddressRepository, UserAddressRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

//Address
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();


//DrugStore
//Product

builder.Services.AddScoped<IProfessionRepository, ProfessionRepository>();
//Quote
builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();

builder.Services.AddSingleton<IQuoteCacheService, QuoteCacheService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
#if !DEBUG
app.UseHttpsRedirection();
#endif

app.UseCors(options =>
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
