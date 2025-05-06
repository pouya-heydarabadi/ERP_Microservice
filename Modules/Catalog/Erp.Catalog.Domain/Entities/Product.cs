using Erp.Catalog.Domain.Common;
using Erp.Catalog.Domain.Events;
using Erp.Catalog.Domain.ValueObjects;

namespace Erp.Catalog.Domain.Entities;

public class Product : Entity
{
    private readonly List<object> _domainEvents = new();
    private readonly List<DynamicField> _dynamicFields = new();
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();
    public IReadOnlyCollection<DynamicField> DynamicFields => _dynamicFields.AsReadOnly();

    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public Money Price { get; private set; }
    public string SKU { get; private set; }
    public bool IsActive { get; private set; }
    public Category Category { get; private set; }
    public Guid CategoryId { get; private set; }

    private Product() { } // For EF Core

    public Product(string name, string code, string description, Money price, string sku, Category category)
    {   
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Product name cannot be empty", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Product code cannot be empty", nameof(code));
        }

        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new ArgumentException("SKU cannot be empty", nameof(sku));
        }

        Name = name;
        Code = code;
        Description = description;
        Price = price;
        SKU = sku;
        Category = category;
        CategoryId = category.Id;
        IsActive = true;
    }

    public void UpdateDetails(string name, string code, string description, Money price)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Product name cannot be empty", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Product code cannot be empty", nameof(code));
        }

        string oldName = Name;
        string oldCode = Code;
        string oldDescription = Description;
        Money oldPrice = Price;
        Guid oldCategoryId = CategoryId;

        Name = name;
        Code = code;
        Description = description;
        Price = price;

        _domainEvents.Add(new ProductUpdatedEvent(
            this,
            oldName,
            name,
            oldCode,
            code,
            oldDescription,
            description,
            oldPrice,
            price,
            oldCategoryId,
            CategoryId));
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void ChangeCategory(Category newCategory)
    {
        Guid oldCategoryId = CategoryId;
        Category = newCategory;
        CategoryId = newCategory.Id;

        _domainEvents.Add(new ProductUpdatedEvent(
            this,
            Name,
            Name,
            Code,
            Code,
            Description,
            Description,
            Price,
            Price,
            oldCategoryId,
            CategoryId));
    }

    public void AddDynamicField(DynamicField field)
    {
        ArgumentNullException.ThrowIfNull(field);

        DynamicField? existingField = _dynamicFields.Find(f => f.Key == field.Key);
        if (existingField != null)
        {
            throw new InvalidOperationException($"Dynamic field with key '{field.Key}' already exists");
        }

        _dynamicFields.Add(field);
    }

    public void UpdateDynamicField(string key, string value)
    {
        DynamicField? field = _dynamicFields.Find(f => f.Key == key);
        if (field == null)
        {
            throw new InvalidOperationException($"Dynamic field with key '{key}' not found");
        }

        field.UpdateValue(value);
    }

    public void RemoveDynamicField(string key)
    {
        DynamicField? field = _dynamicFields.Find(f => f.Key == key);
        if (field == null)
        {
            throw new InvalidOperationException($"Dynamic field with key '{key}' not found");
        }

        _dynamicFields.Remove(field);
    }

    public DynamicField GetDynamicField(string key)
    {
        return _dynamicFields.Find(f => f.Key == key);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
} 
