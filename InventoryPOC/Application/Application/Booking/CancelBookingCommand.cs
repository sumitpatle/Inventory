using Application.Common.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Booking
{
    public class CancelBookingCommand : IRequest<string>
    {
        public int BookingId { get; set; }
    }

    public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, string>
    {
        private readonly IBookingDbContext _context;
        private readonly ILogger<BookItemCommandHandler> _logger;

        public CancelBookingCommandHandler(IBookingDbContext context,
                                      ILogger<BookItemCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<string> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var booking = await _context.BookingsTbls.FindAsync(request.BookingId);

                if (booking == null)
                    return "Booking not found";

                var member = await _context.MembersTbls.FindAsync(booking.MemberId);
                var inventoryItem = await _context.InventoryItemsTbls.FindAsync(booking.InventoryItemId);

                // Remove booking
                _context.BookingsTbls.Remove(booking);
                inventoryItem.RemainingCount += 1;
                member.BookingCount -= 1;

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Booking cancelled successfully");
                return "Booking cancelled successfully";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
                

        }
    }

}
