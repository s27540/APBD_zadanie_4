using WebApplication1.DB;

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



app.UseHttpsRedirection();

app.Run();

