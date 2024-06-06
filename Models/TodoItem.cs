using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Models;

public class TodoItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]    
    public Int64 Id { get; set; }
    public DateTime CreatedAt {get; set; }
    public bool Status {get; set; }

    [NotNull]
    public string Text { get; set; } 

    public DateTime UpdateAt {get; set; }

}