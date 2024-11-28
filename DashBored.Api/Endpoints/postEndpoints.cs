using System;
using DashBored.Api.Data;
using DashBored.Api.DTOs;
using DashBored.Api.Entities;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DashBored.Api.Endpoints;

public static class postEndpoints
{
    const string getPostEndpointName = "getPost";

    // Fix the list initialization
    private static readonly List<postDTO> posts = new List<postDTO>
    {
        new postDTO(
            1,
            "Post1",
            "Desc1",
            new DateOnly(2024, 1, 2)
        ),
        new postDTO(
            2,
            "Post2",
            "Desc2",
            new DateOnly(2020, 2, 10)
        )
    };

    public static RouteGroupBuilder mapPostEndpoints(this WebApplication app)
    {
        // CRUD ENDPOINTS

        var group = app.MapGroup("posts")
                             .WithParameterValidation();

        // GET  /posts
        group.MapGet("/", async (DashBoredContext dbContext) => 
           await dbContext.Posts.AsNoTracking().ToListAsync()); 

        // GET  /posts/id
        group.MapGet("/{id}", async (int id, DashBoredContext dbContext) => 
        {
            Post? post = await dbContext.Posts.FindAsync(id);

            return post is null ? 
                Results.NotFound() : Results.Ok(post);
        })
            .WithName(getPostEndpointName);

        // POST  /posts
        group.MapPost("/", async (createpostDTO newPost, DashBoredContext dbContext) => 
        {
            Post post = new()
            {
                Name = newPost.Name,
                Description = newPost.Description,   
                Date = newPost.Date,
            };

            dbContext.Posts.Add(post);
            await dbContext.SaveChangesAsync();
            
            return Results.CreatedAtRoute(getPostEndpointName, new { id = post.Id }, post);
        });
        
        // PUT  /posts/1
        group.MapPut("/{id}", async (int id, updatepostDTO updatedPost, DashBoredContext dbContext) =>
        {
            var existingPost = await dbContext.Posts.FindAsync(id);

            if (existingPost is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingPost)
                    .CurrentValues
                    .SetValues(updatedPost);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        // DELETE /posts/1
        group.MapDelete("/{id}", async (int id, DashBoredContext dbContext) => 
        {
            await dbContext.Posts.Where(post => post.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
