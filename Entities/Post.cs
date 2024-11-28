using System;

namespace DashBored.Api.Entities;

public class Post
{
    public int Id { get; set;}
    public required string Name { get; set;}
    public required string Description { get; set;}

    public DateOnly Date { get; set;}
}
