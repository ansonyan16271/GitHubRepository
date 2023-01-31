using System;
using System.Collections.Generic;

namespace Advance.NET7.MinimalApi.DB.Models
{
    public partial class NlogManager
    {
        public int Id { get; set; }
        public string Application { get; set; } = null!;
        public DateTime Logged { get; set; }
        public string Level { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string? Logger { get; set; }
        public string? Callsite { get; set; }
        public string? Exception { get; set; }
    }
}
