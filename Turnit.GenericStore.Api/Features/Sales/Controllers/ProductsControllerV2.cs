using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Turnit.GenericStore.Api.Extensions;
using Turnit.GenericStore.Api.Features.Sales.Models;
using Turnit.GenericStore.Api.Models;
using Turnit.GenericStore.Api.Shared;
using TurnitStore.Domain.Entities;
using TurnitStore.Domain.RepositoryInterfaces;
using TurnitStore.Domain.Services;

namespace Turnit.GenericStore.Api.Features.Sales.Controllers;

[Route("v2/products")]
public class ProductsControllerV2(IProductService productService, ICategoryRepo categoryRepo) : ApiControllerBase
{
    [HttpGet]
    public async Task<ProductCategoryModel[]> AllProducts()
    {
        //We might argue about who should be the parent in the relation product => category
        //this can also be achived by queryin the products Where product.Categories contains the categoryId
        var categoriesTask = categoryRepo.GetCategories(includeProducts: true);

        var productsWithoutCategory = await productService.GetUncategorizedProducts();

        var productCategoryModel = (await categoriesTask).Adapt<IEnumerable<ProductCategoryModel>>(MapperConfig.GetConfig);

        productCategoryModel = productCategoryModel.Append(new ProductCategoryModel
        {
            Products = productsWithoutCategory.Adapt<ProductModel[]>()
        });

        return productCategoryModel.ToArray();
    }

    [HttpGet("by-category/{categoryId:guid}")]
    public async Task<IEnumerable<ProductModel>> ProductsByCategory(Guid categoryId)
    {
        //We might argue about who should be the parent in the relation product => category
        //this can also be achived by queryin the products Where product.Categories contains the categoryId
        var categoryProducts = await categoryRepo.GetCategory(categoryId, includeProducts: true);
        var products = categoryProducts.Products.Adapt<IEnumerable<ProductModel>>();
        return products;
    }
       
    [HttpPut("{productId:guid}/category/{categoryId:guid}")]
    public async Task<IActionResult> AssociateToCategory(Guid productId, Guid categoryId)
    {
        var result = await productService.AssociateToCategory(productId, categoryId);
        return result.ToApiResponseModel<Product, ProductModel>();
    }

    [HttpDelete("{productId:guid}/category/{categoryId:guid}")]
    public async Task<IActionResult> DiassociateFromCategory(Guid productId, Guid categoryId)
    {
        var result = await productService.DisassociateFromCategory(productId, categoryId);
        return result.ToApiResponseModel<Product, ProductModel>();
    }

    [HttpPost("{productId:guid}/book")]
    public async Task<IActionResult> Book(Guid productId, IEnumerable<ProductBookingDto> bookings)
    {
        var result = await productService.Book(productId, bookings);
        return result.ToApiResponse();
    }
}