using WebApplication1.Models;

namespace WebApplication1.DB;

public class AppointmentDB
{
    public static List<Appointment> Appointments = new List<Appointment>()
    {
        new Appointment { Date = DateTime.Now, Animal = DB.Animals.First(a => a.Name == "Lili"), Note = "Routine checkup", Price = 50.00 },
        new Appointment { Date = DateTime.Now.AddDays(3), Animal = DB.Animals.First(a => a.Name == "Johnathan"), Note = "Vaccination", Price = 65.00 },
        new Appointment { Date = DateTime.Now.AddDays(5), Animal = DB.Animals.First(a => a.Name == "Viviane"), Note = "Grooming", Price = 30.00 },
        new Appointment { Date = DateTime.Now.AddDays(7), Animal = DB.Animals.First(a => a.Name == "Bob"), Note = "Shell examination", Price = 40.00 },
        new Appointment { Date = DateTime.Now.AddDays(10), Animal = DB.Animals.First(a => a.Name == "Colt"), Note = "Behavioral consultation", Price = 55.00 }
    };
}