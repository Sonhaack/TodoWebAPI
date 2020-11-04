using System.ComponentModel.DataAnnotations;
using Todo_tutorial_2._0.Data;

namespace WebApplication.Models
{
    public class Todo
        {
            [Required]
            [Range(1,int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
            public int userId { get; set; }
            public int id { get; set; }
            [Required, MaxLength(128)]
            public string title { get; set; }
            public bool completed { get; set; }
        }
}