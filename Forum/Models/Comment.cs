using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
