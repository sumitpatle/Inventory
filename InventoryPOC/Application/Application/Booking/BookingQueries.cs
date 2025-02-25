using Application.Common.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Booking
{
    public class BookingQueries : IRequest<IEnumerable<BookingsDTO>>
    {
        public int BookingId { get; set; }
    }

    public class BookingQueriesHandler : IRequestHandler<BookingQueries, IEnumerable<BookingsDTO>>
    {
        private readonly IBookingDbContext _context;
        private readonly ILogger<BookingQueriesHandler> _logger;

        public BookingQueriesHandler(IBookingDbContext context,
                                      ILogger<BookingQueriesHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<BookingsDTO>> Handle(BookingQueries request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<BookingsDTO> result;

                if (request.BookingId == 0)
                {
                    result = await _context.BookingsTbls
                                            .Select(a => new BookingsDTO
                                            {
                                                Id = a.Id,
                                                MemberId = a.MemberId,
                                                InventoryItemId = a.InventoryItemId,
                                                BookingDate = a.BookingDate
                                            })
                                            .ToListAsync(cancellationToken);
                }
                else
                {
                    result = await _context.BookingsTbls
                                            .Where(a => a.Id == request.BookingId)
                                            .Select(a => new BookingsDTO
                                            {
                                                Id = a.Id,
                                                MemberId = a.MemberId,
                                                InventoryItemId = a.InventoryItemId,
                                                BookingDate = a.BookingDate
                                            })
                                            .ToListAsync(cancellationToken);
                }

                _logger.LogInformation($"Fetch Booking");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

    }

}
