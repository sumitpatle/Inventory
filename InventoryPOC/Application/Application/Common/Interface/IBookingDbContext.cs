using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interface
{
    public interface IBookingDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<Member> MembersTbls { get; set; }
        DbSet<InventoryItem> InventoryItemsTbls { get; set; }
        DbSet<Bookings> BookingsTbls { get; set; }
    }
}
