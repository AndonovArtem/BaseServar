using Microsoft.AspNetCore.Mvc;
using Models;
using TodoList.Models.DTO;
using System.ComponentModel.DataAnnotations;
using Models.DTO;
using Repository;

namespace Repository;

public interface IRepository
{   
    public Task AddAsync(TodoItem todoItem);
    public Task SaveAsync();
    public Task<List<TodoItem>> ItemsAsync();
    public void Update(TodoItem todoItem);
    public void UpdateAll(List<TodoItem> todoItems, ChangeStatusTodoDto task);    
    public void DeleteAll(List<TodoItem>  todoItems);
    public void Delete(TodoItem  todoItem);
}