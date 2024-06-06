using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Repository;
using TodoList.Models.DTO;

namespace TodoApi.Controllers;

[ApiController]
[Route("/v1/todo")]
public class TodoController(ITodoService service) : ControllerBase
{
    [HttpGet]
    public CustomSuccessResponse GetPaginated([Required] [Range(1,100)] int page, [Required] [Range(1,100)] int perPage, bool? status)
    {
        try
        {
            var getNewsDto = service.GetTodosAsync(page, perPage, status);

            return new CustomSuccessResponse { 
                Data = getNewsDto.Result,
                StatusCode = 1,
                Success = true,
            };
        }
        catch
        {
            return new CustomSuccessResponse { 
                Data = null,
                StatusCode = 0,
                Success = false,
            };
        }
    }

    [HttpPatch("status/{id}")]
    public async Task<ActionResult<BaseSuccessResponse>> UpdateItemStatus([Required] [Range(1,long.MaxValue)]  Int64 id, [Required] [FromBody] ChangeStatusTodoDto task)
    {
        try
        {
            await service.UpdateStatusAsync(id, task);
            return Ok(new BaseSuccessResponse {
                StatusCode = 1,
                Success = true,
            });
        }
        catch
        {
            var badRequestResponse = new
            {
                success = false, 
                statusCode = 400, // Код состояния Bad Request
                codes = new[] { 400 },
                timeStamp = DateTime.UtcNow.ToString("O") // ISO 8601 формат
            };

            return BadRequest(badRequestResponse);
        }
    }

    [HttpPatch("status")]
    public async Task<ActionResult<BaseSuccessResponse>> UpdateAllItemStatus([Required] [FromBody] ChangeStatusTodoDto status)
    {
        try
        {
            await service.UpdateAllStatusAsync(status);

            return Ok(new BaseSuccessResponse {
                StatusCode = 1,
                Success = true,
            });
        }
        catch
        {
            var badRequestResponse = new
            {
                success = false, 
                statusCode = 400, // Код состояния Bad Request
                codes = new[] { 400 },
                timeStamp = DateTime.UtcNow.ToString("O") // ISO 8601 формат
            };

            return BadRequest(badRequestResponse);
        }
    }
    [HttpDelete]
    public async Task<ActionResult<BaseSuccessResponse>> DeleteAllItem()
    {
        try
        {
            await service.DeleteAllItems();

            return Ok(new BaseSuccessResponse {
                StatusCode = 1,
                Success = true,
            });
        }
        catch (Exception ex)
        {
            var badRequestResponse = new
            {
                success = false, 
                statusCode = ex, // Код состояния Bad Request
                codes = new[] { ex },
                timeStamp = DateTime.UtcNow.ToString("O") // ISO 8601 формат
            };

            return BadRequest(badRequestResponse);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseSuccessResponse>> DeleteItemById([Required] [Range(0, long.MaxValue)] Int64 id)
    {
        try
        {
            await service.DeleteItemById(id);
            
            return Ok(new BaseSuccessResponse {
                StatusCode = 1,
                Success = true,
            });
        }
        catch
        {
            var badRequestResponse = new
            {
                success = false, 
                statusCode = 400, // Код состояния Bad Request
                codes = new[] { 400 },
                timeStamp = DateTime.UtcNow.ToString("O") // ISO 8601 формат
            };

            return BadRequest(badRequestResponse);
        }
    }

    [HttpPatch("text/{id}")]
    public async Task<ActionResult<BaseSuccessResponse>> PatchItemText([Required] [Range(1,long.MaxValue)] Int64 id, [Required] [FromBody] ChangeTextTodoDto task)
    {
        try
        {
            await service.PatchItemText(id, task);
            
            return Ok(new BaseSuccessResponse {
                StatusCode = 1,
                Success = true,
            });
        }
        catch
        {
            var badRequestResponse = new
            {
                success = false, 
                statusCode = 400, // Код состояния Bad Request
                codes = new[] { 400 },
                timeStamp = DateTime.UtcNow.ToString("O") // ISO 8601 формат
            };

            return BadRequest(badRequestResponse);
        }
    }
    [HttpPost]
    public async Task<ActionResult<TodoItem>> CreateTodoItem(CreateTodoDto createdItem)
    {
        var  todoItem = service.CreateTodoItem(createdItem);
        await todoItem;

        return todoItem.Result;
    }
}
