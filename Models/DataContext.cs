﻿namespace quiz_app_backend.Models;

using Microsoft.EntityFrameworkCore;
using quiz_app_backend.Models;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabaseLocal"));
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Score> Scores { get; set; }

}