using Erp.Catalog.Domain.Common;

namespace Erp.Catalog.Domain.ValueObjects;

public class DynamicField : ValueObject
{
    public string Key { get; private set; }
    public string Value { get; private set; }
    public string DataType { get; private set; }
    public bool IsRequired { get; private set; }
    public string DisplayName { get; private set; }
    public int DisplayOrder { get; private set; }

    private DynamicField() { } // For EF Core

    public DynamicField(
        string key,
        string value,
        string dataType,
        bool isRequired,
        string displayName,
        int displayOrder)
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

        Key = key;
        Value = value;
        DataType = dataType;
        IsRequired = isRequired;
        DisplayName = displayName;
        DisplayOrder = displayOrder;
    }

    public void UpdateValue(string newValue)
    {
        if (IsRequired && string.IsNullOrWhiteSpace(newValue))
        {
            throw new ArgumentException("Value cannot be empty for required field", nameof(newValue));
        }

        Value = newValue;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Key;
        yield return Value;
        yield return DataType;
        yield return IsRequired;
        yield return DisplayName;
        yield return DisplayOrder;
    }
} 
