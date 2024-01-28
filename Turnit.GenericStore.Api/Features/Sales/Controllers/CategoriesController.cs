using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Turnit.GenericStore.Api.Models;
using Turnit.GenericStore.Api.Shared;
using TurnitStore.Domain.RepositoryInterfaces;

namespace Turnit.GenericStore.Api.Features.Sales.Controllers;

[Route("categories")]
public class CategoriesController(ICategoryRepo repo) : ApiControllerBase
{
    [HttpGet]
    public async Task<CategoryModel[]> AllCategories()
    {
        var categories = await repo.GetCategories();

        var result = categories
            .Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToArray();

        return result;
    }
}