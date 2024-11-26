using System;
using DashBored.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DashBored.Api.Data;

public class DashBoredContext(DbContextOptions<DashBoredContext> options) : DbContext(options)
{
    public DbSet<Post> Posts => Set<Post>();

    
}
