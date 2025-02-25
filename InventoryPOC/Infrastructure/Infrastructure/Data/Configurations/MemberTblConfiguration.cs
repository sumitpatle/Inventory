using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class MemberTblConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("MEMBER_INFO_TBL");
            builder.HasKey(x => x.Id).IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"MEMBER_ID").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"NAME_TXT").HasColumnType("varchar(50)").IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.Surname).HasColumnName(@"SURNAME_TXT").HasColumnType("varchar(50)").IsRequired().IsUnicode(false).HasMaxLength(1506);
            builder.Property(x => x.BookingCount).HasColumnName(@"BOOKING_COUNT").HasColumnType("bigint").IsRequired();
            builder.Property(x => x.DateJoined).HasColumnName(@"DATE_JOINED").HasColumnType("datetime").IsRequired();
            
            builder.Property(x => x.CREATE_DATETIME).HasColumnName(@"CREATE_DATETIME").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.LAST_UPDATE_DATETIME).HasColumnName(@"LAST_UPDATE_DATETIME").HasColumnType("datetime");
            builder.Property(x => x.CREATE_USER_ID_CD).HasColumnName(@"CREATE_USER_ID_CD").HasColumnType("varchar(125)").IsRequired().IsUnicode(false).HasMaxLength(125);
            builder.Property(x => x.LAST_UPDATE_USER_ID_CD).HasColumnName(@"LAST_UPDATE_USER_ID_CD").HasColumnType("varchar(125)").IsUnicode(false).HasMaxLength(125);
        }
    }
}
