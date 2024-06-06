using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using TodoList.Models.DTO;

namespace Repository;

public class PostgresRepository(TodoContext _todoContext) : IRepository
{
    public async Task AddAsync(TodoItem todoItem)
    {
        await _todoContext.AddAsync(todoItem);
    }

    public async Task SaveAsync()
    {
        await _todoContext.SaveChangesAsync();
    }

    public async Task<List<TodoItem>> ItemsAsync()
    {
        return await _todoContext.TodoItems.ToListAsync();

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