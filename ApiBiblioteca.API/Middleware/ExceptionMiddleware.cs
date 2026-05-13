using ApiBiblioteca.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace ApiBiblioteca.Middleware;

// Foi feito para interceptar os 500 e devolver o status code correto, como 404, 400, etc. E também para devolver uma resposta JSON com a mensagem de erro.
public class ExceptionMiddleware
{
    // RequestDelegate representa o próximo passo do pipeline.
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    // O ASP.NET injeta automaticamente o “próximo da fila”.
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    // Metodo obrigatorio para o middleware, onde a lógica de tratamento de exceção é implementada.
    public async Task InvokeAsync(HttpContext context)
    {
        // Significa: “Execute o resto da aplicação normalmente.”
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            LogException(context, ex);
            await HandleExceptionAsync(context, ex);
        }
    }

    private void LogException(HttpContext context, Exception exception)
    {
        var method = context.Request.Method;
        var path = context.Request.Path;

        if (exception is BadRequestException || exception is NotFoundException)
        {
            // sem passar exception como parâmetro por que está tratando erro esperado como erro técnico
            _logger.LogWarning( 
                "Erro de negócio [{Tipo}] em {Method} {Path}: {Mensagem}",
                exception.GetType().Name,
                method, path,
                exception.Message);
        }
        else
        {
            // aqui sim com stack trace por ser erro interno inesperado
            _logger.LogError(exception, 
                "Erro interno em {Method} {Path}",
                method, path);
        }
    }

    // Decide qual HTTP status devolver
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        // Aqui traduz -> 404, 400, etc. para o tipo HttpStatusCode.
        switch (exception)
        {
            case NotFoundException:
                statusCode = HttpStatusCode.NotFound;
                break;

            case BadRequestException:
                statusCode = HttpStatusCode.BadRequest;
                break;
        }

        // Cria um objeto anônimo para a resposta, contendo o status e a mensagem de erro.
        var response = new
        {
            status = (int)statusCode,
            message = exception.Message
        };

        // Configura a resposta HTTP, definindo o tipo de conteúdo como JSON e o status code apropriado.
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        // Serializa o objeto de resposta para JSON e escreve na resposta HTTP.
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}