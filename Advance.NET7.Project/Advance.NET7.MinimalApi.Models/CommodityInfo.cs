namespace Advance.NET7.MinimalApi.Models
{
    public class CommodityInfo
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreatorId { get; set; }

        public int LastModifierId { get; set; }

        public DateTime LastModifyTime { get; set; }

    }
}