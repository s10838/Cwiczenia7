using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cwiczenia7.Models;

public class Client
{
    [Key]
    public int IdClient { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public DateTime Birthday { get; set; }
    public string Pesel { get; set; }
    public string Email { get; set; }

    [ForeignKey("ClientCategory")]
    public int IdClientCategory { get; set; }
    public ClientCategory ClientCategory { get; set; }

    public ICollection<Reservation> Reservations { get; set; }
}