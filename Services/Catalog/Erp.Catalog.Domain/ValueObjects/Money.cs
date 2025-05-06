using Erp.Catalog.Domain.Common;

namespace Erp.Catalog.Domain.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

    private Money() { } // For EF Core

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative", nameof(amount));
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency cannot be empty", nameof(currency));
        }

        Amount = amount;
        Currency = currency.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
    }

    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency)
        {
            throw new InvalidOperationException("Cannot add money with different currencies");
        }

        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        if (left.Currency != right.Currency)
        {
            throw new InvalidOperationException("Cannot subtract money with different currencies");
        }

        if (left.Amount < right.Amount)
        {
            throw new InvalidOperationException("Result cannot be negative");
        }

        return new Money(left.Amount - right.Amount, left.Currency);
    }

    public static Money operator *(Money money, decimal multiplier)
    {
        if (multiplier < 0)
        {
            throw new ArgumentException("Multiplier cannot be negative", nameof(multiplier));
        }

        return new Money(money.Amount * multiplier, money.Currency);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString()
    {
        return $"{Amount} {Currency}";
    }
} 
