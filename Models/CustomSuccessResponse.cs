using TodoList.Models.DTO;

namespace Models;

public class CustomSuccessResponse : BaseSuccessResponse
{
    public GetNewsDto Data { get; set; }
}