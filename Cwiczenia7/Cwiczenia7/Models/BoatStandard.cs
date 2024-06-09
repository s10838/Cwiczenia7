using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cwiczenia7.Models;

public class BoatStandard
{
    [Key]
    public int IdBoatStandard { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public decimal Price { get; set; } 

    public ICollection<Sailboat> Sailboats { get; set; } = new List<Sailboat>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
