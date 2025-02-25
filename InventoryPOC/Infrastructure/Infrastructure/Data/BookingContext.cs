using Application.Common.Interface;
using Domain.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class BookingContext : DbContext , IBookingDbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }

        public DbSet<Member> MembersTbls { get; set; }
        public DbSet<InventoryItem> InventoryItemsTbls { get; set; }
        public DbSet<Bookings> BookingsTbls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new MemberTblConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryTblConfiguration());
            modelBuilder.ApplyConfiguration(new BookingTblConfiguration());
        }

    }
}
