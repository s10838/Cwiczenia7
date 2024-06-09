using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cwiczenia7.Models;
public class ClientCategory
{
    [Key]
    public int IdClientCategory { get; set; }
    public string Name { get; set; }
    public int DiscountPerc { get; set; }

    public ICollection<Client> Clients { get; set; }
}