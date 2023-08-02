using System.ComponentModel.DataAnnotations;

namespace TattoAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; } = 0;

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Int64 ProductId { get; set; }
    }
}
