using System;

namespace Turnit.GenericStore.Api.Models;

public class ProductModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ProductAvailabilityModel[] Availability { get; set; }

}

public class ProductAvailabilityModel
{
    public Guid StoreId { get; set; }

    public int Availability { get; set; }
}

public class ProductCategoryModel
{
    public Guid? CategoryId { get; set; }

    public ProductModel[] Products { get; set; }
}