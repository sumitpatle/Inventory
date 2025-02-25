using Domain.Common;

namespace Domain.Entities
{
    public class InventoryItem : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RemainingCount { get; set; }
        public DateTime ExpirationDate { get; set; }

        public ICollection<Bookings> BookingsTbls { get; set; }
    }
}
