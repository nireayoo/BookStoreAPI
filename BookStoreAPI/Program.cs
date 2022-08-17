using BookStoreAPI.Data;
using BookStoreAPI.Models;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//private readonly IConfiguration Configuration;
IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

//IConfiguration _configuration = builder.Configuration.GetSection("ConnectionStrings");

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddTransient<IBookRepository, BooksRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();

builder.Services.AddDbContext<BookStoreContext>(options =>
    {
        options.UseSqlServer(_configuration.GetConnectionString("BookStoreDB"));
    });
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BookStoreContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(option =>
{
    option.SaveToken = true;
    option.RequireHttpsMetadata = false;
    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))

    };
}); 


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();
//IConfiguration configuration = app.Configuration;

//public IConfiguration configuration;
//public BookStoreController(IConfiguration configuration)
//{
//    Configuration = configuration;
//}


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
