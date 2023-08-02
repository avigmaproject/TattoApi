using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace TattoAPI.Models
{
    [Keyless]
    public class UserDisplay
    {
        //public string Name { get; set; }
        //public string Email { get; set; }
        //public string? ProductName { get; set; }
        //public string? ProductDescription { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
