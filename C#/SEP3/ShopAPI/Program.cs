using System.Text;
using EfcDataAccess;
using EfcDataAccess.DAOs;
using ShopApplication.DaoInterfaces;
using ShopApplication.Logic;
using ShopApplication.LogicInterfaces;
using FileData;
using FileData.DAOs;
using Shared.Auth;
using Shop.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ShopContext>();
builder.Services.AddScoped<IProductDao, ProductEfcDao>();
builder.Services.AddScoped<IProductLogic, ProductLogic>();

builder.Services.AddScoped<IOrderItemDao, OrderItemsEfcDao>();
builder.Services.AddScoped<IOrderItemLogic, OrderItemLogic>();

builder.Services.AddScoped<IUserDao, UserEfcDao>();
builder.Services.AddScoped<IUserLogic, UserLogic>();


builder.Services.AddScoped<IPurchaseDao, PurchaseEfcDao>();
builder.Services.AddScoped<IPurchaseLogic, PurchaseLogic>();

builder.Services.AddScoped<IOrderDao, OrderEfcDao>();
builder.Services.AddScoped<IOrderLogic, OrderLogic>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{
    // options.RequireHttpsMetadata = false;
    // options.SaveToken = true;
    // options.TokenValidationParameters = new TokenValidationParameters()
    // {
    //     ValidateIssuer = true,
    //     ValidateAudience = true,
    //     ValidAudience = builder.Configuration["Jwt:Audience"],
    //     ValidIssuer = builder.Configuration["Jwt:Issuer"],
    //     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    // };
//});

AuthorizationPolicies.AddPolicies(builder.Services);



builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();