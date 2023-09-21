using GraphFlix.Appsettings;
using GraphFlix.Database;
using GraphFlix.Processors;
using GraphFlix.Repositories;
using GraphFlix.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

string corsPolicy = "_allow";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:7172",
        ValidAudience = "https://localhost:7172",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TestCertificateAndJwtKomNuForHelvedeHvorMangeTegnSkalDerTil"))
    };
});

builder.Services.AddControllers();
builder.Services.Configure<Neo4JSettings>(builder.Configuration.GetSection("Neo4jSettings"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<INeo4J, Neo4J>();
builder.Services.AddScoped<MovieRepository>();

builder.Services.AddTransient<ITokenService, AuthProcessor>();
builder.Services.AddTransient<IHashingService, HashProcessor>();
builder.Services.AddTransient<ISaltService, SaltProcessor>();

builder.Services.AddScoped<IMovieRepository, MockMovieRepository>();
builder.Services.AddScoped<IUserRepository, MockUserRepository>();
//builder.Services.AddScoped<IMovieRepository, Neo4jMovieRepository>();
//builder.Services.AddScoped<IUserRepository, Neo4jUserRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
                      policy =>
                      {
                          policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
