namespace ForensicsCaseLibrary;

public class Case
{
    private decimal BasePrice { get; }
    private static readonly Dictionary<string, decimal> ExhibitPricing = new()
    {
        { "BulletShell", 15 },
        { "Swab", 5 },
        { "Luggage", 150 }
    };

    public string CaseNumber { get; }  // Now set by the library, not manually
    public CaseState State { get; private set; }
    public int CustomerId { get; }
    public string ResponsiblePerson { get; }
    public string CaseType { get; }
    
    private readonly List<Exhibit> _exhibits = new();
    public IReadOnlyList<Exhibit> Exhibits => _exhibits.AsReadOnly();
    public Case(string caseNumber, int customerId, string responsiblePerson, string caseType, decimal basePrice = 50)
    {
        CaseNumber = caseNumber;
        CustomerId = customerId;
        ResponsiblePerson = responsiblePerson;
        CaseType = caseType;
        State = CaseState.New;
        this.BasePrice = basePrice;
    }

    public void AddExhibit(Exhibit exhibit) => _exhibits.Add(exhibit);

    public void Approve()
    {
        State = State switch
        {
            CaseState.Rejected => throw new InvalidOperationException(
                "Cannot approve a case that has already been rejected."),
            CaseState.Completed => throw new InvalidOperationException(
                "Cannot approve a case that has already been completed."),
            _ => CaseState.Approved
        };
    }

    public void Reject()
    {
        if (State == CaseState.Completed)
        {
            throw new InvalidOperationException("Cannot reject a case that has already been completed.");
        }
    
        State = CaseState.Rejected;
    }

    public void PlaceOnHold()
    {
        State = State switch
        {
            CaseState.Rejected => throw new InvalidOperationException(
                "Cannot place on hold a case that has already been rejected."),
            CaseState.Completed => throw new InvalidOperationException(
                "Cannot place on hold a case that has already been completed."),
            _ => CaseState.OnHold
        };
    }
    
    public void Complete()
    {
        State = State switch
        {
            CaseState.Rejected => throw new InvalidOperationException(
                "Cannot complete a case that has already been rejected."),
            CaseState.New => throw new InvalidOperationException(
                "Cannot complete a case that has not been approved."),
            _ => CaseState.Completed
        };
    }

    public decimal CalculateTotalCost()
    {
        int totalExhibitCount = _exhibits.Count;

        decimal totalCost = BasePrice + _exhibits.Sum(exhibit => ExhibitPricing.GetValueOrDefault(exhibit.Type, 0));

        decimal discountMultiplier = 1;
        
        if (totalExhibitCount >= 100)
        {
            discountMultiplier = 0.8m;
        }
        else
        {
            var exhibitCountByType = _exhibits
                .GroupBy(e => e.Type)
                .ToDictionary(g => g.Key, g => g.Count());

            foreach (var exhibitType in exhibitCountByType)
            {
                var count = exhibitType.Value;

                if (count >= 50)
                {
                    discountMultiplier = Math.Min(discountMultiplier, 0.85m);
                }
                else if (count >= 10)
                {
                    discountMultiplier = Math.Min(discountMultiplier, 0.95m);
                }
            }
        }

        totalCost *= discountMultiplier;
        return totalCost;
    }
}
