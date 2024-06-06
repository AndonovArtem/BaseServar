using System.ComponentModel.DataAnnotations;

namespace TodoList.Models.DTO;

public class CreateTodoDto 
{
    [Required]
    [MinLength(3)]
    [MaxLength(160)]
    public string Text { get; set; }

}
