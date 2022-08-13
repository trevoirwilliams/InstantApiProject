using InstantAPIs;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbPath = "./school.db";
var connection = new SqliteConnection($"Data Source={dbPath}");
builder.Services.AddDbContext<SchoolDbContext>(options => { 
    options.UseSqlite(connection);
});

builder.Services.AddInstantAPIs();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapInstantAPIs<SchoolDbContext>();

app.UseHttpsRedirection();

app.Run();

record Student(int Id, string FirstName, string LastName, int SchoolId) 
{ 
    public School? School { get; set; } 
};

record School(int Id, string Name);

class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    {

    }

    public DbSet<Student> Students { get; set; }
    public DbSet<School> Schools { get; set; }
}