using Application.Common.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Booking
{
    public class MemberQueries : IRequest<IEnumerable<MemberDTO>>
    {
    }

    public class MemberQueriesHandler : IRequestHandler<MemberQueries, IEnumerable<MemberDTO>>
    {
        private readonly IBookingDbContext _context;
        private readonly ILogger<MemberQueriesHandler> _logger;

        public MemberQueriesHandler(IBookingDbContext context,
                                      ILogger<MemberQueriesHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<MemberDTO>> Handle(MemberQueries query, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<MemberDTO> result;

                result = await _context.MembersTbls
                                        .Select(a => new MemberDTO
                                        {
                                            Id = a.Id,
                                            Name = a.Name,
                                            Surname = a.Surname,
                                            BookingCount = a.BookingCount,
                                            DateJoined   = a.DateJoined
                                        })
                                        .ToListAsync(cancellationToken);


                _logger.LogInformation($"Fetch Members");

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
