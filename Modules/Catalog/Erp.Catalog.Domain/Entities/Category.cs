using Erp.Catalog.Domain.Common;
using Erp.Catalog.Domain.ValueObjects;

namespace Erp.Catalog.Domain.Entities;

public class Category : Entity
{
    private readonly List<Category> _subCategories = new List<Category>();
    private readonly List<DynamicField> _dynamicFields = new List<DynamicField>();
    public IReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();
    public IReadOnlyCollection<DynamicField> DynamicFields => _dynamicFields.AsReadOnly();

    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public Category? ParentCategory { get; private set; }
    public Guid? ParentCategoryId { get; private set; }

    private Category() { } // For EF Core

    public Category(string name, string description, Category? parentCategory = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category name cannot be empty", nameof(name));
        }

        Name = name;
        Description = description;
        ParentCategory = parentCategory;
        ParentCategoryId = parentCategory?.Id;
        IsActive = true;
    }

    public void UpdateDetails(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category name cannot be empty", nameof(name));
        }

        Name = name;
        Description = description;
    }

    public void AddSubCategory(Category subCategory)
    {
        ArgumentNullException.ThrowIfNull(subCategory);

        if (subCategory.Id == Id)
        {
            throw new InvalidOperationException("A category cannot be a subcategory of itself");
        }

        _subCategories.Add(subCategory);
    }

    public void RemoveSubCategory(Category subCategory)
    {
        ArgumentNullException.ThrowIfNull(subCategory);

        _subCategories.Remove(subCategory);
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void ChangeParentCategory(Category newParentCategory)
    {
        if (newParentCategory != null && newParentCategory.Id == Id)
        {
            throw new InvalidOperationException("A category cannot be its own parent");
        }

        ParentCategory = newParentCategory;
        ParentCategoryId = newParentCategory?.Id;
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
} 
