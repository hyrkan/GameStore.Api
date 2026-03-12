using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record UpdateGameDto(
    [Required] [StringLength(50)]
    string Name,

    string Genre,

    decimal Price,
    
    DateOnly ReleaseDate
);
