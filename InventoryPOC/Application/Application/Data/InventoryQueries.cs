using Application.Common.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Booking
{
    public class InventoryQueries : IRequest<IEnumerable<InventoryDTO>>
    {
    }

    public class InventoryQueriesHandler : IRequestHandler<InventoryQueries, IEnumerable<InventoryDTO>>
    {
        private readonly IBookingDbContext _context;
        private readonly ILogger<InventoryQueriesHandler> _logger;

        public InventoryQueriesHandler(IBookingDbContext context,
                                      ILogger<InventoryQueriesHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<InventoryDTO>> Handle(InventoryQueries query, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<InventoryDTO> result;

                result = await _context.InventoryItemsTbls  
                                        .Select(a => new InventoryDTO
                                        {
                                            Id = a.Id,
                                            Title = a.Title,
                                            Description = a.Description,
                                            RemainingCount = a.RemainingCount,
                                            ExpirationDate = a.ExpirationDate
                                        })
                                        .ToListAsync(cancellationToken);


                _logger.LogInformation($"Fetch Inventory");

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
