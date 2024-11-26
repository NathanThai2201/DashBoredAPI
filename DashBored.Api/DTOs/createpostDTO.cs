using System.ComponentModel.DataAnnotations;

namespace DashBored.Api.DTOs;

public record class createpostDTO(
    [Required][StringLength(50)] String Name, 
    [Required][StringLength(200)] String Description,
    DateOnly Date
);
