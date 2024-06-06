using Microsoft.AspNetCore.Mvc;
using Models;
using TodoList.Models.DTO;
using System.ComponentModel.DataAnnotations;
using Models.DTO;
using Repository;

namespace Repository;

public interface IRepository
{   
    public void Add(TodoItem todoItem);
    public void Save();
    public List<TodoItem> Items();
    public void Update(TodoItem todoItem);
    public void UpdateAll(List<TodoItem> todoItems, ChangeStatusTodoDto task);    
    public void DeleteAll(List<TodoItem>  todoItems);
    public void Delete(TodoItem  todoItem);
}