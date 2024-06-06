using Models;

namespace TodoList.Models.DTO;

public class GetNewsDto
{
    public List<TodoItem> Content { get; set; }
    public Int64 NotReady { get; set; }
    public Int64 Ready { get; set; }
    public Int64 NumberOfElements { get; set; }
}

