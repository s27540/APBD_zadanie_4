namespace WebApplication1.Models;

public class Appointment
{
    public DateTime Date { get; set; }
    public Animal Animal { get; set; }
    public string Note { get; set; }
    public double Price { get; set; }
}