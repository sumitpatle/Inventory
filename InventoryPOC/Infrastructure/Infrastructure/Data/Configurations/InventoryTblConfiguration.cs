using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class InventoryTblConfiguration : IEntityTypeConfiguration<InventoryItem>
    {
        public void Configure(EntityTypeBuilder<InventoryItem> builder)
        {
            builder.ToTable("INVENTORY_INFO_TBL");
            builder.HasKey(x => x.Id).IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"INVENTORY_ID").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Title).HasColumnName(@"TITLE_TXT").HasColumnType("varchar(50)").IsRequired().IsUnicode(false).HasMaxLength(16);
            builder.Property(x => x.Description).HasColumnName(@"DISCRIPTION_TXT").HasColumnType("varchar(50)").IsRequired().IsUnicode(false).HasMaxLength(16);
            builder.Property(x => x.RemainingCount).HasColumnName(@"REMAINING_COUNT").HasColumnType("bigint");
            builder.Property(x => x.ExpirationDate).HasColumnName(@"EXPIRATION_DATE").HasColumnType("datetime").IsRequired();
            
            builder.Property(x => x.CREATE_DATETIME).HasColumnName(@"CREATE_DATETIME").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.LAST_UPDATE_DATETIME).HasColumnName(@"LAST_UPDATE_DATETIME").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.CREATE_USER_ID_CD).HasColumnName(@"CREATE_USER_ID_CD").HasColumnType("varchar(125)").IsRequired().IsUnicode(false).HasMaxLength(125);
            builder.Property(x => x.LAST_UPDATE_USER_ID_CD).HasColumnName(@"LAST_UPDATE_USER_ID_CD").HasColumnType("varchar(125)").IsRequired().IsUnicode(false).HasMaxLength(125);

            builder.HasIndex(x => new { x.Id}).HasDatabaseName("sqlite_autoindex_INVENTORY_INFO_TBL_1").IsUnique();
        }
    }
}
