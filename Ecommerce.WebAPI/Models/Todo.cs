using System.ComponentModel.DataAnnotations;

namespace Ecommerce.WebAPI.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        public string? TodoName { get; set; }
    }
}
