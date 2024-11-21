using System;
using DashBored.Api.DTOs;

namespace DashBored.Api.Endpoints;

public static class postEndpoints
{
    const string getPostEndpointName = "getPost";
    private static readonly List<postDTO> posts = [
        new (
            1,
            "Post1",
            "Desc1",
            new DateOnly(2024,1,2)
        ), new (
            2,
            "Post2",
            "Desc2",
            new DateOnly(2020,2,10)
        )];
    public static RouteGroupBuilder mapPostEndpoints(this WebApplication app){
        // CRUD ENDPOINTS

        var group = app.MapGroup("posts")
                             .WithParameterValidation();

        // GET  /posts
        group.MapGet("/", () => posts); 

        // GET  /posts/id
        group.MapGet("/{id}", (int id) => 
        {
            postDTO? post = posts.Find(post => post.Id == id);

            return post is null ? Results.NotFound() : Results.Ok(post);
        })
            .WithName(getPostEndpointName);

        // POST  /posts
        group.MapPost("/", (createpostDTO newPost) => 
        {

            postDTO post = new(
                posts.Count + 1,
                newPost.Name,
                newPost.Description,
                newPost.Date
            );

            posts.Add(post);

            return Results.CreatedAtRoute(getPostEndpointName, new {id = post.Id}, post);
        });
        
        // PUT  /posts/1
        group.MapPut("/{id}", (int id, updatepostDTO updatedPost) =>
        {
            var index = posts.FindIndex(post => post.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            posts[index] = new postDTO(
                id,
                updatedPost.Name,
                updatedPost.Description,
                updatedPost.Date
            );

            return Results.NoContent();
        });

        // DELETE /posts/1
        group.MapDelete("/{id}", (int id) => 
        {
            posts.RemoveAll(post => post.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
