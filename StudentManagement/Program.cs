using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StudentManagement.DataAccess;
using StudentManagement.Services.Token;
using StudentManagement.Services.User;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(
               options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               }
               ).AddJwtBearer(options =>
               {
                   var key = Encoding.UTF8.GetBytes("this is my custom Secret key for authentication");
                   options.SaveToken = true;
                   options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       /*ValidIssuer = ["JWT:Issuer"],
                       ValidAudience = ["Jwt:Audience"],*/
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                       ValidateLifetime = true,
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });

builder.Services.AddCors(options => options.AddPolicy("Cors", builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserService>();
builder.Services.AddScoped<IJWTRepository, JWTManager>();
builder.Services.AddScoped<SMDbContext>();
var app = builder.Build();

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
