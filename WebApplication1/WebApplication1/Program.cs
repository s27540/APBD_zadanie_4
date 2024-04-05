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


//GET /api/animals
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

//GET /api/animals/{id:int}
app.MapGet("/api/animals/{id:int}", (int id) =>
{
    var animal = DB.animals.Find(a => a.Id == id);

    if (animal != null)
    {
        return Results.Ok(animal);
    }
    else
    {
        return Results.NotFound($"Animal with id {id} not found");
    }
});

//POST /api/animals
app.MapPost("/api/animals", ([FromBody] Animal animal) =>
{
    if (DB.animals.Any(a => a.Id == animal.Id))
    {
        return Results.Conflict($"Animal with id {animal.Id} already exists");
    }
    
    DB.animals.Add(animal);
    return Results.Created($"/api/animals/{animal.Id}", animal);
});

// PUT /api/animals/{id:int}
app.MapPut("/api/animals/{id:int}", (int id, [FromBody] Animal animalInserted) =>
{
    var animal = DB.animals.FirstOrDefault(a => a.Id == id);
    if (animal is null) return Results.NotFound($"Animal with id {id} not found");

    
    if (!animalInserted.Name.Equals("string") && animal.Name != animalInserted.Name)
        animal.Name = animalInserted.Name;

    if (!animalInserted.Category.Equals("string") && animal.Category != animalInserted.Category)
        animal.Category = animalInserted.Category;

    if (animalInserted.Mass != 0 && animal.Mass != animalInserted.Mass)
        animal.Mass = animalInserted.Mass;

    if (!animalInserted.CoatColor.Equals("string") && animal.CoatColor != animalInserted.CoatColor)
        animal.CoatColor = animalInserted.CoatColor;

    return Results.Ok(animal);
});






app.UseHttpsRedirection();

app.Run();

