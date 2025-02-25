namespace Application.Upload
{
    public class InventoryItemDto
    {
        public string title { get; set; }
        public string description { get; set; }
        public int remaining_count { get; set; }
        public string expiration_date { get; set; }
    }

}
