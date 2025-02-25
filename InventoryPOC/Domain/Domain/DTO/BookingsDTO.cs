namespace Domain.Entities
{
    public class BookingsDTO
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int InventoryItemId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
