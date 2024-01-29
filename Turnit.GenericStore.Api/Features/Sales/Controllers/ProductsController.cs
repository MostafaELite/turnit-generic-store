using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using Turnit.GenericStore.Api.Entities;
using Turnit.GenericStore.Api.Extensions;
using Turnit.GenericStore.Api.Features.Sales.Models;
using Turnit.GenericStore.Api.Models;
using Turnit.GenericStore.Api.Shared;
using TurnitStore.Domain.Entities;
using TurnitStore.Domain.RepositoryInterfaces;
using TurnitStore.Domain.Services;

namespace Turnit.GenericStore.Api.Features.Sales.Controllers;

[Route("products")]
public class ProductsController(ISession session) : ApiControllerBase
{
    [HttpGet("by-category/{categoryId:guid}")]
    public async Task<ProductModel[]> ProductsByCategory(Guid categoryId)
    {
        var products = await session.QueryOver<ProductCategory>()
            .Where(x => x.Category.Id == categoryId)
            .Select(x => x.Product)
            .ListAsync<Product>();

        var result = new List<ProductModel>();

        foreach (var product in products)
        {
            var availability = await session.QueryOver<ProductAvailability>()
                .Where(x => x.Product.Id == product.Id)
                .ListAsync();

            var model = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Availability = availability.Select(x => new ProductAvailabilityModel
                {
                    StoreId = x.Store.Id,
                    Availability = x.Availability
                }).ToArray()
            };
            result.Add(model);
        }

        return result.ToArray();
    }

    [HttpGet]
    public async Task<ProductCategoryModel[]> AllProducts()
    {
        var products = await session.QueryOver<Product>().ListAsync<Product>();
        var w = session.GetHashCode();
        var productModels = new List<ProductModel>();
        foreach (var product in products)
        {
            var availability = await session.QueryOver<ProductAvailability>()
                .Where(x => x.Product.Id == product.Id)
                .ListAsync();

            var model = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Availability = availability.Select(x => new ProductAvailabilityModel
                {
                    StoreId = x.Store.Id,
                    Availability = x.Availability
                }).ToArray()
            };
            productModels.Add(model);
        }


        var productCategories = await session.QueryOver<ProductCategory>().ListAsync();
        var result = new List<ProductCategoryModel>();
        foreach (var category in productCategories.GroupBy(x => x.Category.Id))
        {
            var productIds = category.Select(x => x.Product.Id).ToHashSet();
            result.Add(new ProductCategoryModel
            {
                CategoryId = category.Key,
                Products = productModels
                    .Where(x => productIds.Contains(x.Id))
                    .ToArray()
            });
        }

        var uncategorizedProducts = productModels.Except(result.SelectMany(x => x.Products));
        if (uncategorizedProducts.Any())
        {
            result.Add(new ProductCategoryModel
            {
                Products = uncategorizedProducts.ToArray()
            });
        }

        return result.ToArray();
    }
}