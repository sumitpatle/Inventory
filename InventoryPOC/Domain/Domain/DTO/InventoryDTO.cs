﻿using Domain.Common;

namespace Domain.Entities
{
    public class InventoryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RemainingCount { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
