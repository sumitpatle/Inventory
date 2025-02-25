namespace Domain.Common
{
    public abstract class AuditableEntity
    {
        public required string CREATE_USER_ID_CD { get; set; }

        public DateTime? CREATE_DATETIME { get; set; }

        public string? LAST_UPDATE_USER_ID_CD { get; set; }

        public DateTime? LAST_UPDATE_DATETIME { get; set; }
    }
}
