using Models;
using Models.DTO;
using TodoList.Models.DTO;

public interface ITodoService 
{
    Task<GetNewsDto> GetTodosAsync(int page, int perPage, bool? status);
    Task UpdateStatusAsync(Int64 id, ChangeStatusTodoDto status);
    Task UpdateAllStatusAsync(ChangeStatusTodoDto status);
    Task DeleteItemById(Int64 id);
    Task DeleteAllItems();
    Task PatchItemText(Int64 id, ChangeTextTodoDto task);
    Task<TodoItem> CreateTodoItem(CreateTodoDto createTodoDto);
}