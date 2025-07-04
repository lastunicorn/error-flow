# Error Flow (ASP.NET Core)

This library offers an easy way to handle exceptions in a ASP.NET Core application and transform them into a HTTP response.

## Usage

### 1) Register error handlers

Register the error handlers into the dependency container:

```c#
builder.Services.AddErrorFlow(configuration =>
{
    Assembly presentationAssembly = ...;
    configuration.AddErrorHandlersFromAssembly(presentationAssembly);
});
```

### 2) Add middleware

Add the middleware that handles the errors.

```c#
app.UseErrorFlow();
```

### 3) Create error handlers

Create a handler class for each exception that needs to be handled.

Specify the HTTP Status Code to be returned and the body object to be serialized as JSON.

```c#
public class DummyErrorHandler : JsonErrorHandler<DummyException, ErrorResponseDto>
{
    protected override HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

    protected override ErrorResponseDto BuildHttpResponseBody(DummyException ex)
    {
        return new ErrorResponseDto
        {
            ErrorMessage = ex.Message
        };
    }
}
```

