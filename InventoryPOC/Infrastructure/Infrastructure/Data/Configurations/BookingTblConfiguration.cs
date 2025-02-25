using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class BookingTblConfiguration : IEntityTypeConfiguration<Bookings>
    {
        public void Configure(EntityTypeBuilder<Bookings> builder)
        {
            builder.ToTable("BOOKING_INFO_TBL");
            builder.HasKey(x => x.Id).IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"BOOKING_ID").HasColumnType("long").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.MemberId).HasColumnName(@"MEMBER_ID").HasColumnType("long").IsRequired();
            builder.Property(x => x.InventoryItemId).HasColumnName(@"INVENTORY_ID").HasColumnType("long").IsRequired();

            builder.Property(x => x.BookingDate).HasColumnName(@"BOOKING_DATE").HasColumnType("datetime").IsRequired();
            
            builder.Property(x => x.CREATE_DATETIME).HasColumnName(@"CREATE_DATETIME").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.LAST_UPDATE_DATETIME).HasColumnName(@"LAST_UPDATE_DATETIME").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.CREATE_USER_ID_CD).HasColumnName(@"CREATE_USER_ID_CD").HasColumnType("varchar(125)").IsRequired().IsUnicode(false).HasMaxLength(125);
            builder.Property(x => x.LAST_UPDATE_USER_ID_CD).HasColumnName(@"LAST_UPDATE_USER_ID_CD").HasColumnType("varchar(125)").IsRequired().IsUnicode(false).HasMaxLength(125);

            builder.HasIndex(x => new { x.Id}).HasDatabaseName("sqlite_autoindex_BOOKING_INFO_TBL").IsUnique();

            builder.HasOne(a => a.Member).WithMany(b => b.BookingsTbls).HasForeignKey(c => c.MemberId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_BOOKING_INFO_TBL_1");
            builder.HasOne(a => a.InventoryItem).WithMany(b => b.BookingsTbls).HasForeignKey(c => c.InventoryItemId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_BOOKING_INFO_TBL_2");
        }
    }
}
