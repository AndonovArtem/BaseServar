using Models;
using Models.DTO;
using TodoList.Models.DTO;

public interface ITodoService 
{
    GetNewsDto GetTodos(int page, int perPage, bool? status);
    void UpdateStatus(Int64 id, ChangeStatusTodoDto status);
    void UpdateAllStatus(ChangeStatusTodoDto status);
    void DeleteItemById(Int64 id);
    void DeleteAllItems();
    void PatchItemText(Int64 id, ChangeTextTodoDto task);
    TodoItem CreateTodoItem(CreateTodoDto createTodoDto);
}