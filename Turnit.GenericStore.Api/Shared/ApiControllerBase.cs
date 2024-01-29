using Microsoft.AspNetCore.Mvc;

namespace Turnit.GenericStore.Api.Shared;

[SessionDisposalFilter]
[ApiController]
[Produces("application/json")]
public abstract class ApiControllerBase : ControllerBase
{

}