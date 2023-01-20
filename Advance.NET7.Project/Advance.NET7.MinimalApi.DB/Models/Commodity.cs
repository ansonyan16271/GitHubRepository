using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
 

namespace Advance.NET7.MinimalApi.DB.Models
{ 
    [Table("Commodity")]
    public partial class Commodity
    {
        public int Id { get; set; }
        public long? ProductId { get; set; }
        public int? CategoryId { get; set; } 
        [Column("Title")]
        public string? Title { get; set; }
        public decimal? Price { get; set; }
        public string? Url { get; set; }
        public string? ImageUrl { get; set; }
    }
}
