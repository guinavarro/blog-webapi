
using Blog.WebApi.Infra;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddCors();

//builder.Services.AddAuthentication(_ =>
//{
//    _.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    _.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    _.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(_ =>
//{
//    // Como vai ser validado a chave
//    _.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidIssuer = config["JwtSettings:Issuer"],
//        ValidAudience = config["JwtSettings:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true, 
//        ValidateIssuerSigningKey = true
//    };
//});

builder.Services.AddAuthorization();

builder.Services.AddControllers();

var connection = config["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<BlogContext>(options =>
{
    options.UseNpgsql(connection,
        assembly => assembly.MigrationsAssembly(typeof(BlogContext).Assembly.FullName));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ResolveDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
