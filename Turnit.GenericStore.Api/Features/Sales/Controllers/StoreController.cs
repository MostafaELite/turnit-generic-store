using System;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Turnit.GenericStore.Api.Extensions;
using Turnit.GenericStore.Api.Features.Sales.Models;
using Turnit.GenericStore.Api.Models;
using Turnit.GenericStore.Api.Shared;
using TurnitStore.Domain.Enums;
using TurnitStore.Domain.RepositoryInterfaces;
using TurnitStore.Domain.Services;

namespace Turnit.GenericStore.Api.Features.Sales.Controllers;

[Route("store")]
public class StoreController(IStoreService stores) : ApiControllerBase
{
    /// <summary>
    /// Addes the given quantity to the available product quantity for each product in the store
    /// </summary>    
    /// <returns>the new store products availability</returns>
    [HttpPost("{storeId}/restock")]
    public async Task<IActionResult> Restock(Guid storeId, RestockRequest restock)
    {
        var result = await stores.RestockAsync(storeId, restock.ProductId, restock.Quantity);

        if (result.Status != ResultStatus.Success)
            result.ToApiResponse();

        var apiModel = result.Result.Adapt<ProductAvailabilityModel>();
        return Ok(apiModel);

    }
}