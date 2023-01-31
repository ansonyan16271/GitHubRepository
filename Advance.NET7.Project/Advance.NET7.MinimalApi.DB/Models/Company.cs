using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advance.NET7.MinimalApi.DB.Models
{

    [Table("Company")]
    public partial class Company
    {
        public int Id { get; set; }

        [Column("Name")]
        public string? Name { get; set; }
        [Column("CreateTime")]
        public DateTime? CreateTime { get; set; }
        public int CreatorId { get; set; }
        public int? LastModifierId { get; set; }
        public DateTime? LastModifyTime { get; set; }
    }
}
