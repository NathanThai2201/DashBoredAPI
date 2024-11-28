namespace DashBored.Api.DTOs;

public record class postDTO(
    int Id, 
    String Name, 
    String Description,
    DateOnly Date
);
