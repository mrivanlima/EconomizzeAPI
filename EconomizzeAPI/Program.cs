using Economizze.Library;
using EconomizzeAPI.Services.Cache;
using EconomizzeAPI.Services.DBServices;
using EconomizzeAPI.Services.Repositories;
using EconomizzeAPI.Services.Repositories.Classes;
using EconomizzeAPI.Services.Repositories.Interfaces;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(options =>
{
	options.ReturnHttpNotAcceptable = true;
}).AddXmlSerializerFormatters();

builder.Services.AddScoped<IConnectionService, ConnectionService>();




//User
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserGroupRepository, UserGroupRepository>();
builder.Services.AddScoped<IUserLoginRepository, UserLoginRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserTokenRepository, UserTokenRepository>();
builder.Services.AddScoped<IUserAddressRepository, UserAddressRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

//Address
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAddressTypeRepository, AddressTypeRepository>();
builder.Services.AddScoped<INeighborhoodRepository, NeighborhoodRepository>();
builder.Services.AddScoped<IStreetRepository, StreetRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();



builder.Services.AddScoped<IContactTypeRepository, ContactTypeRepository>();

//DrugStore
builder.Services.AddScoped<IDrugstoreNeighborhoodSubscriptionRepository, DrugstoreNeighborhoodSubscriptionRepository>();
builder.Services.AddScoped<IDrugstoreRepository, DrugstoreRepository>();
//Product
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductVersionRepository, ProductVersionRepository>();

builder.Services.AddScoped<IProfessionRepository, ProfessionRepository>();
//Quote
builder.Services.AddScoped<IQuoteProductRepository, QuoteProductRepository>();
builder.Services.AddScoped<IQuoteProductResponseRepository, QuoteProductResponseRepository>();

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

app.UseCors("AllowBlazorOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
