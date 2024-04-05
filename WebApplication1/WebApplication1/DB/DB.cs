using WebApplication1.Models;

namespace WebApplication1.DB;

public class DB
{
    public static List<Animal> animals = new()
    {
        new Animal { Id = 1, Name = "Lili", Category = "Dog", CoatColor = "White", Mass = 23.5},
        new Animal { Id = 2, Name = "Johnathan", Category = "Dog", CoatColor = "Black", Mass = 43.6},
        new Animal { Id = 3, Name = "Viviane", Category = "Cat", CoatColor = "Red and White", Mass = 15.4},
        new Animal { Id = 4, Name = "Bob", Category = "Turtle", CoatColor = "Sand", Mass = 65.8},
        new Animal { Id = 5, Name = "Colt", Category = "Fox", CoatColor = "Red", Mass = 46.3}
    };
}