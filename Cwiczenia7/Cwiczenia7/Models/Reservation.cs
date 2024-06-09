using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cwiczenia7.Models;

public class Reservation
{
    [Key]
    public int IdReservation { get; set; }

    [ForeignKey("Client")]
    public int IdClient { get; set; }
    public Client Client { get; set; }

    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }

    [ForeignKey("BoatStandard")]
    public int IdBoatStandard { get; set; }
    public BoatStandard BoatStandard { get; set; }

    public int Capacity { get; set; }
    public int NumOfBoats { get; set; }
    public bool Fulfilled { get; set; }
    public decimal Price { get; set; }
    public string CancelReason { get; set; }

    public ICollection<SailboatReservation> SailboatReservations { get; set; }
}