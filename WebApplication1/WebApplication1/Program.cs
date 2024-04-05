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
    if (DB.Animals.Any())
    {
        return Results.Ok(DB.Animals);
    }
    else
    {
        return Results.NotFound("Animals not found");
    }
});

//GET /api/animals/{id:int}
app.MapGet("/api/animals/{id:int}", (int id) =>
{
    var animal = DB.Animals.Find(a => a.Id == id);

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
    if (DB.Animals.Any(a => a.Id == animal.Id))
    {
        return Results.Conflict($"Animal with id {animal.Id} already exists");
    }
    
    DB.Animals.Add(animal);
    return Results.Created($"/api/animals/{animal.Id}", animal);
});

// PUT /api/animals/{id:int}
app.MapPut("/api/animals/{id:int}", (int id, [FromBody] Animal animalInserted) =>
{
    var animal = DB.Animals.FirstOrDefault(a => a.Id == id);
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

//Delete /api/animals/{id:int}
app.MapDelete("/api/animals/{id:int}", (int id) =>
{
    var animal = DB.Animals.FirstOrDefault(a => a.Id == id);
    if (animal == null)
    {
        return Results.NotFound($"Animal with id {id} not found");
    }
    
    var animals = DB.Animals.Where(a => a.Id != id);
    DB.Animals = animals.ToList();
    return Results.Ok();
});

//GET /api/animals/appointments
app.MapGet("/api/animals/appointments", () =>
{
    if (AppointmentDB.Appointments.Any())
    {
        return Results.Ok(AppointmentDB.Appointments);
    }
    else
    {
        return Results.NotFound("Appointments not found");
    }
});

//GET /api/animals/{id:int}/appointments
app.MapGet("/api/animals/{id:int}/appointments", (int id) =>
{
    var animal = DB.Animals.FirstOrDefault(a => a.Id == id);
    if (animal == null)
    {
        return Results.NotFound($"Animal with id {id} not found");
    }

    var appointmentsList = AppointmentDB.Appointments.Where(a => a.Animal.Id == id);

    if (appointmentsList == null)
    {
        return Results.NotFound($"Appointments for animal with id {id} not found");
    }
    
    return Results.Ok(appointmentsList);
});

//POST /api/animals/{id:int}/appointments
app.MapPost("/api/animals/{id:int}/appointments", (int id, [FromBody] Appointment appointment) =>
{
    var animal = DB.Animals.FirstOrDefault(a => a.Id == id);
    if (animal == null)
    {
        return Results.NotFound($"Animal with id {id} not found");
    }

    if (AppointmentDB.Appointments.Any(a => a.Id == appointment.Id))
    {
        return Results.Conflict($"Appointment with id {appointment.Id} already exists");
    }

    if (id != appointment.Animal.Id)
    {
        return Results.Conflict($"Animal id in url {id} not equal to animal id {appointment.Animal.Id} in appointment");
    }
    
    AppointmentDB.Appointments.Add(appointment);
    return Results.Created($"/api/animals/{id}/appointments", appointment);
});

app.UseHttpsRedirection();

app.Run();

