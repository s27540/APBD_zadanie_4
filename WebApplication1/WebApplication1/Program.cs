using Microsoft.AspNetCore.Mvc;
using WebApplication1.DB;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//Get animal List
app.MapGet("/api/animals", () =>
{
    if (DB.animals.Any())
    {
        return Results.Ok(DB.animals);
    }
    else
    {
        return Results.NotFound("Animals not found");
    }
});

//Get animal by id
app.MapGet("/api/animals/{id:int}", (int id) =>
{
    var animal = DB.animals.Find(a => a.Id == id);

    if (animal != null)
    {
        return Results.Ok(animal);
    }
    else
    {
        return Results.NotFound("Animal with that Id not found");
    }
});

//Adding animal
app.MapPost("/api/animals", ([FromBody] Animal animal) =>
{
    if (DB.animals.Any(a => a.Id == animal.Id))
    {
        return Results.Conflict("Animal with that Id already exists");
    }
    
    DB.animals.Add(animal);
    return Results.Created($"/api/animals/{animal.Id}", animal);
});





app.UseHttpsRedirection();

app.Run();

