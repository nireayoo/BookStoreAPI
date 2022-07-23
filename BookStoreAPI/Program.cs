using BookStoreAPI.Data;
using BookStoreAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//private readonly IConfiguration Configuration;
IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

//IConfiguration _configuration = builder.Configuration.GetSection("ConnectionStrings");

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddTransient<IBookRepository, BooksRepository>();

builder.Services.AddDbContext<BookStoreContext>(options =>
    {
        options.UseSqlServer(_configuration.GetConnectionString("BookStoreDB"));
    });
builder.Services.AddAutoMapper(typeof(Program));

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

app.MapControllers();

app.Run();
