using Models;
using Models.DTO;
using Repository;
using TodoList.Models.DTO;

public class TodoService : ITodoService
{
    private readonly IRepository db;

    public TodoService(IRepository db)
    {
        this.db = db;
    }

    public async Task<GetNewsDto> GetTodosAsync(int page, int perPage, bool? status)
    {
        
        List<TodoItem> todoItems = new List<TodoItem>();
        
        if(status == null)
        {
            var asyncItems = db.ItemsAsync();
            await asyncItems;

            todoItems = asyncItems.Result.Skip((page - 1) * perPage).Take(perPage).ToList();
        }
        else if(status == true)
        {
            var asyncItems = db.ItemsAsync();
            await asyncItems;

            todoItems = asyncItems.Result.Skip((page - 1) * perPage).Take(perPage).Where(i => i.Status == true).ToList();
        }
        else if(status == false)
        {
            var asyncItems = db.ItemsAsync();
            await asyncItems;

            todoItems = asyncItems.Result.Skip((page - 1) * perPage).Where(i => i.Status == false).Take(perPage).ToList();
        }

        GetNewsDto getNewsDto = new GetNewsDto()
        {
            Content = todoItems,
            Ready = db.ItemsAsync().Result.Where(i => i.Status == true).Count(),
            NotReady = db.ItemsAsync().Result.Where(i => i.Status == false).Count(),
            NumberOfElements = db.ItemsAsync().Result.Count(),
        };   
        

        return getNewsDto;
    }
    
    public async Task UpdateStatusAsync(Int64 id, ChangeStatusTodoDto status)
    {
        TodoItem? todoItem = db.ItemsAsync().Result.Where(i => i.Id == id).FirstOrDefault();

        todoItem.Status = status.Status;
   
        db.Update(todoItem);
        await db.SaveAsync();
    }

    public async Task UpdateAllStatusAsync(ChangeStatusTodoDto status)
    {
        db.UpdateAll(db.ItemsAsync().Result, status);
        await db.SaveAsync();
    }

    public async Task DeleteItemById(Int64 id)
    {
        TodoItem? todoItem = db.ItemsAsync().Result.Where(i => i.Id == id).FirstOrDefault();

        db.Delete(todoItem);
        await db.SaveAsync();
    }

    public async Task DeleteAllItems()
    {
        db.DeleteAll(db.ItemsAsync().Result);
        await db.SaveAsync();
    }
    
    public async Task PatchItemText(Int64 id, ChangeTextTodoDto task)
    {
        TodoItem? todoItem = db.ItemsAsync().Result.Where(i => i.Id == id).FirstOrDefault();

        todoItem.Text = task.Text;

        db.Update(todoItem);
        await db.SaveAsync();
    }

    public async Task<TodoItem> CreateTodoItem(CreateTodoDto createTodoDto)
    {
        var todoItem = DtoToItem(createTodoDto);

        await db.AddAsync(todoItem);
        await db.SaveAsync();

        return todoItem;
    }

    private TodoItem DtoToItem(CreateTodoDto createTodoDto) =>
        new TodoItem
        {
            UpdateAt = DateTime.Now.ToUniversalTime(),
            CreatedAt = DateTime.Now.ToUniversalTime(),
            Status = false,
            Text = createTodoDto.Text,
        };
}