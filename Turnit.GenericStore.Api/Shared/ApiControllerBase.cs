using Microsoft.AspNetCore.Mvc;
using Turnit.GenericStore.Api.Middlewares;

namespace Turnit.GenericStore.Api.Shared;

[SessionDisposalFilter]
[ApiController]
[Produces("application/json")]
public abstract class ApiControllerBase : ControllerBase
{

}