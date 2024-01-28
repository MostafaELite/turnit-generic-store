using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;
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

[Route("v2/products")]
public class ProductsControllerV2(IProductService productService, ICategoryRepo categoryRepo) : ApiControllerBase
{
    [HttpGet]
    public async Task<ProductCategoryModel[]> AllProducts()
    {
        var categoriesTask = categoryRepo.GetCategories(includeProducts: true);

        var productsWithoutCategory = await productService.GetUncategorizedProducts();

        var productCategoryModel = TinyMapper.Map<IEnumerable<ProductCategoryModel>>(await categoriesTask);

        productCategoryModel = productCategoryModel.Append(new ProductCategoryModel
        {
            Products = TinyMapper.Map<ProductModel[]>(productsWithoutCategory)
        });

        return productCategoryModel.ToArray();
    }

    [HttpGet("by-category/{categoryId:guid}")]
    public async Task<IEnumerable<ProductModel>> ProductsByCategory(Guid categoryId)
    {
        //We might argue about who should be the parent in the relation product => category
        //this can also be achived by queryin the products Where product.Categories contains the categoryId
        var categoryProducts = await categoryRepo.GetCategory(categoryId);
        var products = TinyMapper.Map<IEnumerable<ProductModel>>(categoryProducts.Products);
        return products;
        //TODO: availabilty
    }


    [HttpPut("{productId:guid}/category/{categoryId:guid}")]
    public async Task<IActionResult> AssociateToCategory(Guid productId, Guid categoryId)
    {
        var result = await productService.AssociateToCategory(productId, categoryId);
        return result.ToApiResponse();
    }

    [HttpDelete("{productId:guid}/category/{categoryId:guid}")]
    public async Task<IActionResult> DiassociateFromCategory(Guid productId, Guid categoryId)
    {
        var result = await productService.DisassociateFromCategory(productId, categoryId);
        return result.ToApiResponse();
    }

    [HttpPost("{productId:guid}/book")]
    public async Task<IActionResult> Book(Guid productId, ProductBookingRequest request)
    {
        await productService.Book(productId, request.Quantity, request.PerferedStoreId);
        return Ok();
    }
}