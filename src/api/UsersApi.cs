using Microsoft.AspNetCore.Mvc;
using StoreApi.src.application;
using StoreApi.src.application.DTOs;

namespace StoreApi.src.api
{
    public static class UsersApi
    {
        public static void MapUserEndPoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/api/register", async (CreateUserDTO createUserDTO, AddUserUseCase addUserUseCase) =>
            {
                try
                {
                    var response = await addUserUseCase.ExecuteAsync(createUserDTO);
                    return Results.Ok(response);
                }
                catch (ArgumentException ex) { return Results.BadRequest(new { message = ex.Message }); }
            });

            routes.MapPost("/api/login", async (LoginUserDTO loginUserDTO, LoginUserUseCase loginUserUseCase) =>
            {
                try
                {
                    var response = await loginUserUseCase.ExecuteAsync(loginUserDTO);
                    if (response == null) { return Results.Forbid(); }
                    else { return Results.Ok(response); }
                }
                catch (ArgumentException ex) { return Results.BadRequest(new { message = ex.Message }); }

            });
        }
    }
}