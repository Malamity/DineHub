using HotChocolate.Execution;
using HotChocolate.Language;
using Microsoft.AspNetCore.Mvc;

namespace GraphQL.Controllers;


[Route("graphql")]
[ApiController]
public class GraphQlController : ControllerBase
{
    private readonly IRequestExecutor _requestExecutor;

    public GraphQlController(IRequestExecutorResolver executorResolver)
    {
        _requestExecutor = executorResolver.GetRequestExecutorAsync().Result;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GraphQLRequest request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        var result = await _requestExecutor.ExecuteAsync(
            QueryRequestBuilder.New()
                .SetQuery(request.Query!)
                .SetVariableValues(request.Variables)
                .Create());

        return Ok(result);
    }
}
