using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cwiczenia7.Models;

public class Sailboat
{
    [Key]
    public int IdSailboat { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }

    [ForeignKey("BoatStandard")]
    public int IdBoatStandard { get; set; }
    public BoatStandard BoatStandard { get; set; }
    public decimal Price { get; set; }

    public ICollection<SailboatReservation> SailboatReservations { get; set; }
}
