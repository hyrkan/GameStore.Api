using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoint
{
    const string GetGameEnpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new (1, "Street Fighter", "Fighting", 19.99M, new DateOnly(1992,7,15))  
    ];

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        group.MapGet("/", () => games);

        group.MapGet("/{id}", 
        (int id) =>
        {
            var game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEnpointName);

        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new (
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate

            );
            
            games.Add(game);
            return Results.CreatedAtRoute(GetGameEnpointName, new{id = game.Id}, game);
        });

        // update is expecting 2 parameters, one for id and second is for the updated data
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) => {

            var index = games.FindIndex(game => game.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }
            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            var result = games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });

    }
}   
