using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using TodoList.Models.DTO;

namespace Repository;

public class PostgresRepository(TodoContext _todoContext) : IRepository
{
    public void Add(TodoItem todoItem)
    {
        _todoContext.Add(todoItem);
    }

    public void Save()
    {
        _todoContext.SaveChanges();
    }

    public List<TodoItem> Items()
    {
        return _todoContext.TodoItems.ToList();
    }

    public void Update(TodoItem todoItem)
    {
        _todoContext.TodoItems.Update(todoItem);
    }

    public void UpdateAll(List<TodoItem> todoItems, ChangeStatusTodoDto task)
    {
        foreach (TodoItem item in todoItems)
        {
            item.Status = task.Status;
            Update(item);
        }
    }
    
    public void DeleteAll(List<TodoItem>  todoItems)
    {
        _todoContext.TodoItems.RemoveRange(todoItems);
    }

    public void Delete(TodoItem  todoItem)
    {
        _todoContext.TodoItems.Remove(todoItem);
    }
}