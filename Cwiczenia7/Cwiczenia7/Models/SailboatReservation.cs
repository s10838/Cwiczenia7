using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cwiczenia7.Models;

public class SailboatReservation
{
    [ForeignKey("Sailboat")]
    public int IdSailboat { get; set; }
    public Sailboat Sailboat { get; set; }

    [ForeignKey("Reservation")]
    public int IdReservation { get; set; }
    public Reservation Reservation { get; set; }
}