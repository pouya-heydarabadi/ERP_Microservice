using Erp.Catalog.Domain.Entities;
using Erp.Catalog.Domain.ValueObjects;

namespace Erp.Catalog.Domain.Events;

public class ProductUpdatedEvent
{
    public Guid ProductId { get; }
    public string ProductName { get; }
    public string OldName { get; }
    public string NewName { get; }
    public string OldCode { get; }
    public string NewCode { get; }
    public string OldDescription { get; }
    public string NewDescription { get; }
    public Money OldPrice { get; }
    public Money NewPrice { get; }
    public Guid? OldCategoryId { get; }
    public Guid? NewCategoryId { get; }
    public DateTime OccurredOn { get; }

    public ProductUpdatedEvent(
        Product product,
        string oldName,
        string newName,
        string oldCode,
        string newCode,
        string oldDescription,
        string newDescription,
        Money oldPrice,
        Money newPrice,
        Guid? oldCategoryId,
        Guid? newCategoryId)
    {
        ArgumentNullException.ThrowIfNull(product);

        ProductId = product.Id;
        ProductName = product.Name;
        OldName = oldName;
        NewName = newName;
        OldCode = oldCode;
        NewCode = newCode;
        OldDescription = oldDescription;
        NewDescription = newDescription;
        OldPrice = oldPrice;
        NewPrice = newPrice;
        OldCategoryId = oldCategoryId;
        NewCategoryId = newCategoryId;
        OccurredOn = DateTime.UtcNow;
    }

    public bool HasNameChanged => OldName != NewName;
    public bool HasCodeChanged => OldCode != NewCode;
    public bool HasDescriptionChanged => OldDescription != NewDescription;
    public bool HasPriceChanged => !OldPrice.Equals(NewPrice);
    public bool HasCategoryChanged => OldCategoryId != NewCategoryId;
} 
