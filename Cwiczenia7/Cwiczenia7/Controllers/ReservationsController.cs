using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenia7.Context;
using Cwiczenia7.Models;

namespace Cwiczenia7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostReservation([FromBody] ReservationRequest request)
        {
            // Check for active reservation
            var activeReservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.IdClient == request.IdClient && !r.Fulfilled);

            if (activeReservation != null)
            {
                return BadRequest("Client already has an active reservation.");
            }

            // Check for available boats
            var boatStandard = await _context.BoatStandards.FindAsync(request.IdBoatStandard);
            var higherStandardBoats = await _context.Sailboats
                .Where(s => s.BoatStandard.Level > boatStandard.Level)
                .CountAsync();

            if (higherStandardBoats < request.NumOfBoats)
            {
                var newReservation = new Reservation
                {
                    IdClient = request.IdClient,
                    DateFrom = request.DateFrom,
                    DateTo = request.DateTo,
                    IdBoatStandard = request.IdBoatStandard,
                    NumOfBoats = request.NumOfBoats,
                    Fulfilled = false,
                    CancelReason = "Not enough boats available"
                };

                _context.Reservations.Add(newReservation);
                await _context.SaveChangesAsync();

                return BadRequest("Not enough boats available. Reservation marked as canceled.");
            }

            // Create reservation
            var reservation = new Reservation
            {
                IdClient = request.IdClient,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                IdBoatStandard = request.IdBoatStandard,
                NumOfBoats = request.NumOfBoats,
                Fulfilled = true,
                Price = 0m // Calculate based on business logic
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            // Calculate the price
            var client = await _context.Clients
                .Include(c => c.ClientCategory)
                .FirstOrDefaultAsync(c => c.IdClient == request.IdClient);

            reservation.Price = CalculateReservationPrice(request.IdBoatStandard, client.ClientCategory.DiscountPerc);

            await _context.SaveChangesAsync();

            return Ok(reservation.IdReservation);
        }

        private decimal CalculateReservationPrice(int boatStandardId, int discountPercentage)
        {
            var boatStandardPrice = _context.BoatStandards
                .Where(bs => bs.IdBoatStandard == boatStandardId)
                .Select(bs => bs.Price)
                .FirstOrDefault();

            return boatStandardPrice * (1 - discountPercentage / 100m);
        }
    }

    public class ReservationRequest
    {
        public int IdClient { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int IdBoatStandard { get; set; }
        public int NumOfBoats { get; set; }
    }
}
