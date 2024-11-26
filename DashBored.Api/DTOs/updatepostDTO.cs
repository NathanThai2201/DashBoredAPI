using System.ComponentModel.DataAnnotations;
namespace DashBored.Api.DTOs;

public record class updatepostDTO(
    [Required][StringLength(50)] String Name, 
    [Required][StringLength(200)] String Description,
    DateOnly Date
);

