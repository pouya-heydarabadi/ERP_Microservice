using Erp.Catalog.Domain.Common;

namespace Erp.Catalog.Domain.Entities;

public class DynamicFieldTemplate : Entity
{
    public string Key { get; private set; }
    public string DataType { get; private set; }
    public bool IsRequired { get; private set; }
    public string DisplayName { get; private set; }
    public int DisplayOrder { get; private set; }
    public string DefaultValue { get; private set; }
    public string ValidationRules { get; private set; }
    public bool IsActive { get; private set; }
    public string EntityType { get; private set; } // "Product" or "Category"
    public Guid? CategoryId { get; private set; } // If null, applies to all categories

    private DynamicFieldTemplate() { } // For EF Core

    public DynamicFieldTemplate(
        string key,
        string dataType,
        bool isRequired,
        string displayName,
        int displayOrder,
        string defaultValue,
        string validationRules,
        string entityType,
        Guid? categoryId = null)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be empty", nameof(key));
        }

        if (string.IsNullOrWhiteSpace(dataType))
        {
            throw new ArgumentException("Data type cannot be empty", nameof(dataType));
        }

        if (string.IsNullOrWhiteSpace(displayName))
        {
            throw new ArgumentException("Display name cannot be empty", nameof(displayName));
        }

        if (string.IsNullOrWhiteSpace(entityType))
        {
            throw new ArgumentException("Entity type cannot be empty", nameof(entityType));
        }

        Key = key;
        DataType = dataType;
        IsRequired = isRequired;
        DisplayName = displayName;
        DisplayOrder = displayOrder;
        DefaultValue = defaultValue;
        ValidationRules = validationRules;
        EntityType = entityType;
        CategoryId = categoryId;
        IsActive = true;
    }

    public void UpdateDetails(
        string dataType,
        bool isRequired,
        string displayName,
        int displayOrder,
        string defaultValue,
        string validationRules)
    {
        if (string.IsNullOrWhiteSpace(dataType))
        {
            throw new ArgumentException("Data type cannot be empty", nameof(dataType));
        }

        if (string.IsNullOrWhiteSpace(displayName))
        {
            throw new ArgumentException("Display name cannot be empty", nameof(displayName));
        }

        DataType = dataType;
        IsRequired = isRequired;
        DisplayName = displayName;
        DisplayOrder = displayOrder;
        DefaultValue = defaultValue;
        ValidationRules = validationRules;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void ChangeCategory(Guid? categoryId)
    {
        CategoryId = categoryId;
    }
} 