using Domain.Common;

namespace Domain.Entities
{
    public class Bookings : AuditableEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int InventoryItemId { get; set; }
        public DateTime BookingDate { get; set; }

        public Member Member { get; set; }
        public InventoryItem InventoryItem { get; set; }
    }
}
