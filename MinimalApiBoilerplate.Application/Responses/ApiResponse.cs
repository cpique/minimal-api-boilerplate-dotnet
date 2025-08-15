using System.Net;

namespace MinimalApiBoilerplate.Application.Responses;

public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public object? Result { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public List<string> ErrorMessages { get; set; } = [];
}
