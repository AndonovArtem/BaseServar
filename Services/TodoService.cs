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

    public GetNewsDto GetTodos(int page, int perPage, bool? status)
    {
        GetNewsDto getNewsDto = new GetNewsDto();   
        List<TodoItem> todoItems = new List<TodoItem>();
        
        if(status == null)
        {
            todoItems = db.Items().Skip((page - 1) * perPage).Take(perPage).ToList();
        }
        else if(status == true)
        {
            todoItems = db.Items().Skip((page - 1) * perPage).Take(perPage).Where(i => i.Status == true).ToList();
        }
        else if(status == false)
        {
            todoItems = db.Items().Skip((page - 1) * perPage).Where(i => i.Status == false).Take(perPage).ToList();
        }
        
        getNewsDto.Content = todoItems;
        getNewsDto.Ready = db.Items().Where(i => i.Status == true).Count();
        getNewsDto.NotReady = db.Items().Where(i => i.Status == false).Count();
        getNewsDto.NumberOfElements = db.Items().Count();

        return getNewsDto;
    }
    
    public void UpdateStatus(Int64 id, ChangeStatusTodoDto status)
    {
        TodoItem todoItem = db.Items().Where(i => i.Id == id).FirstOrDefault();

        todoItem.Status = status.Status;
   
        db.Update(todoItem);
        db.Save();
    }

    public void UpdateAllStatus(ChangeStatusTodoDto status)
    {
        db.UpdateAll(db.Items(), status);
        db.Save();
    }

    public void DeleteItemById(Int64 id)
    {
        TodoItem todoItem = db.Items().Where(i => i.Id == id).FirstOrDefault();

        db.Delete(todoItem);
        db.Save();
    }

    public void DeleteAllItems()
    {
        db.DeleteAll(db.Items());
        db.Save();
    }
    
    public void PatchItemText(Int64 id, ChangeTextTodoDto task)
    {
        TodoItem todoItem = db.Items().Where(i => i.Id == id).FirstOrDefault();

        todoItem.Text = task.Text;

        db.Update(todoItem);
        db.Save();
    }

    public TodoItem CreateTodoItem(CreateTodoDto createTodoDto)
    {
        var todoItem = DtoToItem(createTodoDto);

        db.Add(todoItem);
        db.Save();

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