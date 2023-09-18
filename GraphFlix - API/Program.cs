using GraphFlix.Appsettings;
using GraphFlix.Database;
using GraphFlix.Managers;
using GraphFlix.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<Neo4JSettings>(builder.Configuration.GetSection("Neo4jSettings"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<INeo4J, Neo4J>();
builder.Services.AddScoped<MovieRepository>();

builder.Services.AddScoped<IMovieManager, MovieManager>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IMovieRepository, MockMovieRepository>();
//builder.Services.AddScoped<IMovieRepository, Neo4jMovieRepository>();
//builder.Services.AddScoped<IUserRepository, Neo4jUserRepository>();

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
