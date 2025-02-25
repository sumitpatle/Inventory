using Application.Common.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Booking
{
    public class BookItemCommand : IRequest<string>
    {
        public int MemberId { get; set; }
        public int InventoryItemId { get; set; }
    }

    public class BookItemCommandHandler : IRequestHandler<BookItemCommand, string>
    {
        private readonly IBookingDbContext _context;
        private readonly ILogger<BookItemCommandHandler> _logger;

        public BookItemCommandHandler(IBookingDbContext context,
                                      ILogger<BookItemCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<string> Handle(BookItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var member = await _context.MembersTbls.FindAsync(request.MemberId);
                var inventoryItem = await _context.InventoryItemsTbls.FindAsync(request.InventoryItemId);

                if (member == null || inventoryItem == null)
                    return "Member or Inventory Item not found";

                if (member.BookingCount >= 2)
                    return "Member has reached maximum bookings";

                if (inventoryItem.RemainingCount <= 0)
                    return "Inventory item is out of stock";

                var bookings = _context.BookingsTbls.ToList();
                var bookingId = bookings.Any() ? bookings.Select(x => x.Id).Max() : 0;
                bookingId++;
                // Create Booking
                var booking = new Bookings
                {
                    Id = bookingId,
                    MemberId = request.MemberId,
                    InventoryItemId = request.InventoryItemId,
                    BookingDate = DateTime.Now,
                    CREATE_USER_ID_CD = "Test_User",
                    LAST_UPDATE_USER_ID_CD = "Test_User",
                    CREATE_DATETIME = DateTime.Now,
                    LAST_UPDATE_DATETIME = DateTime.Now
                };

                inventoryItem.RemainingCount -= 1;
                member.BookingCount += 1;

                _context.BookingsTbls.Add(booking);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Booking successful");
                return "Booking successful";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }

}
