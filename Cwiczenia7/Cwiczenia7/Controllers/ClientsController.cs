using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenia7.Context;
using Cwiczenia7.Models;

namespace Cwiczenia7.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}/reservations")]
    public async Task<ActionResult<Client>> GetClientReservations(int id)
    {
        var client = await _context.Clients
            .Include(c => c.Reservations)
            .ThenInclude(r => r.BoatStandard)
            .FirstOrDefaultAsync(c => c.IdClient == id);

        if (client == null)
        {
            return NotFound();
        }

        client.Reservations = client.Reservations.OrderByDescending(r => r.DateTo).ToList();
        return client;
    }
}
