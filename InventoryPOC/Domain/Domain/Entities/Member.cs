using Domain.Common;

namespace Domain.Entities
{
    public class Member : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BookingCount { get; set; }
        public DateTime DateJoined { get; set; }

        public ICollection<Bookings> BookingsTbls { get; set; }

    }
}
